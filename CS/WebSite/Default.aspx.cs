using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraPrinting;
using System.IO;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = ASPxGridViewExporter1;
            using (MemoryStream ms = new MemoryStream())
            {
                link.CreateDocument(false);
                BrickEnumerator brickEnum = link.PrintingSystem.Document.Pages[0].GetEnumerator();
                float maxWidth=0;
                while(brickEnum.MoveNext())
                    maxWidth = maxWidth > brickEnum.CurrentBrick.Rect.X + brickEnum.CurrentBrick.Rect.Width ? maxWidth : brickEnum.CurrentBrick.Rect.X + brickEnum.CurrentBrick.Rect.Width;
                float freeSpace = (link.PrintingSystem.Document.Pages[0]).PageSize.Width - maxWidth;
                if (freeSpace > 600)
                {
                    link.Margins.Left = (int)(freeSpace / 6);
                    link.CreateDocument(false);
                }
                link.ExportToPdf(ms);
                WriteDataToResponse(ms.ToArray(), ExportType.PDF);
            }

        }
          public static void WriteDataToResponse(byte[] data, ExportType type)
        {
            if (data != null && data.Length > 0 && type != ExportType.none)
            {
                String fileEnding = String.Empty;
                String fileContent = String.Empty;
                switch (type)
                {
                    case ExportType.XLS:
                        fileContent = "application/ms-excel";
                        fileEnding = "xls";
                        break;
                    case ExportType.PDF:
                        fileContent = "application/pdf";
                        fileEnding = "pdf";
                        break;
                    case ExportType.CSV:
                        fileContent = "text/plain";
                        fileEnding = "csv";
                        break;
                    case ExportType.RTF:
                        fileContent = "text/enriched";
                        fileEnding = "rtf";
                        break;
                }
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.AppendHeader("Content-Type", fileContent);
                HttpContext.Current.Response.AppendHeader("Content-Transfer-Encoding", "binary");
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "inline; filename=Export." + fileEnding);
                HttpContext.Current.Response.BinaryWrite(data);
                HttpContext.Current.Response.End();
            }
        }
    }
    
 public enum ExportType
    {
        none = 0,
        XLS = 1,
        PDF = 2,
        CSV = 4,
        RTF = 8
    }

}
