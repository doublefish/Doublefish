using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

namespace DoubleFish.Web.View.Excel
{
	public partial class ImportExcel : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{

		}

		protected void btnUpload_Click (object sender, EventArgs e)
		{
			var postedFile = fileUpload.PostedFile;

			var ds = LoadDataFromExcel(postedFile.FileName);

			dg1.DataSource = ds.Tables[0].DefaultView;
			dg1.DataBind();
		}

		//加载Excel
		public static DataSet LoadDataFromExcel (string filePath)
		{
			try
			{
				string strConn;
				strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";
				OleDbConnection OleConn = new OleDbConnection(strConn);
				OleConn.Open();
				String sql = "SELECT * FROM [Sheet1$]";

				OleDbDataAdapter OleDaExcel = new OleDbDataAdapter(sql, OleConn);
				DataSet OleDsExcle = new DataSet();
				OleDaExcel.Fill(OleDsExcle, "Sheet1");
				OleConn.Close();
				return OleDsExcle;
			}
			catch (Exception)
			{

				return null;
			}
		}
	}
}