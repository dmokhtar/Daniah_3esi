using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3esi_Website
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            bool fileOK = false;
            String path = Server.MapPath("~/UploadedCSVFiles/");

            if (this.CSVFileUpload.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(CSVFileUpload.FileName).ToLower();
                String[] allowedExtensions = { ".csv" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }

                if (fileOK)
                {
                    try
                    {
                        CSVFileUpload.PostedFile.SaveAs(path + CSVFileUpload.FileName);
                        Label1.Text = "File uploaded!";
                    }
                    catch (Exception ex)
                    {
                        Label1.Text = "File could not be uploaded.";
                    }
                }
                else
                {
                    Label1.Text = "Cannot accept files of this type.";
                }
            }
        }
    }
}