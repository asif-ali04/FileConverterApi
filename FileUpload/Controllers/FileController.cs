using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using FileUpload.Models;

namespace FileUpload.Controllers
{
	public class FileController : ApiController
    {
		[Route("api/file/upload")]
		public IHttpActionResult ConvertFileToCSV()
		{
			string path = HttpContext.Current.Server.MapPath("~/Uploads/");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			//Fetch the File.
			HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
			
			//Fetch the File Name.
				string fileName = HttpContext.Current.Request.Form["fileName"] + Path.GetExtension(postedFile.FileName);

			//Save the File.
			postedFile.SaveAs(path + fileName);
			List<Employee> employees = new List<Employee>();
			XDocument doc = XDocument.Load(path + fileName);
			foreach (XElement element in doc.Descendants("documentelement")
				.Descendants("employee"))
			{
				Employee employee = new Employee();
				employee.ID = element.Element("id").Value;
				employee.Cmp_Name = element.Element("cmp_name").Value;
				employee.Address = element.Element("address").Value;
				employees.Add(employee);
			}
			//return employees;

		return Ok(employees);
		}
	}
}
