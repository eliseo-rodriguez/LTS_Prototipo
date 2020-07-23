using LTS_Proto.BL.Models;
using LTS_Proto.DL;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using System.IO;

namespace LTS_Proto.BL.BO {
    public class UsrBO {
        private readonly IUsrDM UsrDM;

        public UsrBO(IUsrDM usrDM) {
            UsrDM = usrDM;
        }
        public UsrModel Auth(string usr, string psw) {
            UsrModel lxUsr = UsrDM.Auth(usr, psw);

            return lxUsr;
        }
        public void Brr(string UsrId) {
            UsrDM.Brr(UsrId);
        }
        public async Task<int> Cre(UsrModel usr) {
            try {
                var lxRows = await UsrDM.Cre(usr);
                return lxRows;
            } catch(Exception ex) { 
                throw ex;
            }
        }
        public void Grd(UsrModel usr) {
            UsrDM.Grd(usr);
        }

        public UsrModel Obt(string usr) {
            UsrModel lxUsr = UsrDM.Obt(usr);
            return lxUsr;
        }

        public async Task<IEnumerable<UsrModel>> Lst_Obt() {
            return await UsrDM.Lst_Obt();
        }

        public MemoryStream Pdf(IEnumerable<UsrModel> lstUsrs) {
            if(lstUsrs == null) {
                throw new ArgumentException("Lst Usr no puede ser null");
            }

            using(PdfDocument pdfDocument = new PdfDocument()) {
                int paragraphAfterSpacing = 8;
                int cellMargin = 8;

                //Add page to the PDF document
                PdfPage page = pdfDocument.Pages.Add();

                //Create a new font
                PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 16);

                //Create a text element to draw a text in PDF page
                PdfTextElement title = new PdfTextElement("Usuarios", font, PdfBrushes.Black);
                PdfLayoutResult result = title.Draw(page, new PointF(0, 0));

                PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

                PdfTextElement content = new PdfTextElement("Lista de usuarios.", contentFont, PdfBrushes.Black);
                PdfLayoutFormat format = new PdfLayoutFormat();
                format.Layout = PdfLayoutType.Paginate;

                //Draw a text to the PDF document
                result = content.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, 
                                                              page.GetClientSize().Width, page.GetClientSize().Height), format);

                //Create a PdfGrid
                PdfGrid pdfGrid = new PdfGrid();
                pdfGrid.Style.CellPadding.Left = cellMargin;
                pdfGrid.Style.CellPadding.Right = cellMargin;

                //Applying built-in style to the PDF grid
                pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

                //Assign data source
                pdfGrid.DataSource = lstUsrs;

                pdfGrid.Style.Font = contentFont;

                //Draw PDF grid into the PDF page
                pdfGrid.Draw(page, new PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                using(MemoryStream stream = new MemoryStream()) {
                    //Saving the PDF document into the stream
                    pdfDocument.Save(stream);
                    //Closing the PDF document
                    pdfDocument.Close(true);
                    return stream;
                }
            }
        }
    }
}
