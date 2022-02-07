using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test
{
	class APIreciver
	{
		public static Book ApiRequest(string request)
		{
			WebRequest wrGETURL;
			wrGETURL = WebRequest.Create(request);
			Stream objStream;
			objStream = wrGETURL.GetResponse().GetResponseStream();
			StreamReader objReader = new StreamReader(objStream);

			/*string sLine = "";
			int i = 0;*/
			Book book = JsonSerializer.Deserialize<Book>(objStream);
			//Console.WriteLine(book.docs[0].public_scan_b);

			return book;
		}
	}
	 public class Book
	{
		public int numFound { get; set; }
		
		public Doc[] docs { get; set; }

		public Book(int numFound,  Doc[] doc)
		{
			docs = doc;
			this.numFound = numFound;
			
		}
		public Book()
		{

		}

	}
	public class Doc
	{
		public string title { get; set; }
		public string [] author_name { get; set; }
		public int edition_count { get; set; }
		public int first_publish_year { get; set; }
		public int number_of_pages_median { get; set; }
		public bool public_scan_b { get; set; }



		public Doc(string title, string[] author_name, int edition_count, int first_publish_year, int number_of_pages_median, bool public_scan_b)
		{
			this.title = title;
			this.author_name = author_name;
			this.edition_count = edition_count;
			this.first_publish_year = first_publish_year;
			this.number_of_pages_median = number_of_pages_median;
			this.public_scan_b = public_scan_b;

		}
	/*	public Doc(string title, int edition_count, int first_publish_year, int number_of_pages_median, bool public_scan_b)
		{
			this.title = title;
			this.edition_count = edition_count;
			this.first_publish_year = first_publish_year;
			this.number_of_pages_median = number_of_pages_median;
			this.public_scan_b = public_scan_b;

		}*/

	}
	


}
