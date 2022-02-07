using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Events;
using LiveCharts.Defaults;
using System.Windows.Media;
using System.Threading;

namespace Test
{
	public partial class Form1 : Form
	{
		List<string> list_with_autor_connections = new List<string>();
		SeriesCollection series;
		public Book temporaryBook;
		private SqlConnection sqlConnection = null;
		//-------------------------------------------------------------------
		ChartValues<ScatterPoint> science = new ChartValues<ScatterPoint>();
		ChartValues<ScatterPoint> Nscience = new ChartValues<ScatterPoint>();
		ChartValues<ScatterPoint> programming = new ChartValues<ScatterPoint>();
		ChartValues<ScatterPoint> Nprogramminng = new ChartValues<ScatterPoint>();
		ChartValues<ScatterPoint> fantasy = new ChartValues<ScatterPoint>();
		ChartValues<ScatterPoint> Nfantasy = new ChartValues<ScatterPoint>();
		ChartValues<ScatterPoint> classical_literature = new ChartValues<ScatterPoint>();
		ChartValues<ScatterPoint> Nclassical_literature = new ChartValues<ScatterPoint>();
		ChartValues<ScatterPoint> science_fiction = new ChartValues<ScatterPoint>();
		ChartValues<ScatterPoint> Nscience_fiction = new ChartValues<ScatterPoint>();

		ScatterSeries Science = new ScatterSeries
			{
				Title="Science (With public scan)",
				MinPointShapeDiameter = 10,
				MaxPointShapeDiameter = 60,
				PointGeometry = DefaultGeometries.Circle,
				Fill = Brushes.Blue
			};
			ScatterSeries NotScience = new ScatterSeries
			{
				Title = "Science (Without public scan)",
				MinPointShapeDiameter = 10,
				MaxPointShapeDiameter = 60,
				PointGeometry = DefaultGeometries.Triangle,
				Fill = Brushes.Blue
			};

			ScatterSeries Classical_literature = new ScatterSeries
			{
				Title = "Classical Literature (With public scan)",
				MinPointShapeDiameter = 10,
				MaxPointShapeDiameter = 60,
				PointGeometry = DefaultGeometries.Circle,
				Fill = Brushes.Cyan
			};
		ScatterSeries NotClassical_literature = new ScatterSeries
		{
			Title = "Classical Literature (Without public scan)",
			MinPointShapeDiameter = 10,
				MaxPointShapeDiameter = 60,
				PointGeometry = DefaultGeometries.Triangle,
				Fill = Brushes.Cyan
				
			};
			ScatterSeries Fantasy = new ScatterSeries
			{
				Title = "Fantasy (With public scan)",
				MinPointShapeDiameter = 10,
				MaxPointShapeDiameter = 60,
				PointGeometry = DefaultGeometries.Circle,
				Fill = Brushes.Chartreuse
			};
			ScatterSeries NotFantasy = new ScatterSeries
			{
				Title = "Fantasy (Without public scan)",
				MinPointShapeDiameter = 10,
				MaxPointShapeDiameter = 60,
				PointGeometry = DefaultGeometries.Triangle,
				Fill = Brushes.Chartreuse
			};
			ScatterSeries Programming = new ScatterSeries
			{
				Title = "Programming (With public scan)",
				MinPointShapeDiameter = 10,
				MaxPointShapeDiameter = 60,
				PointGeometry = DefaultGeometries.Circle,
				Fill = Brushes.DarkOrchid
			};
			ScatterSeries NotProgarmming = new ScatterSeries
			{
				Title = "Programming (Without public scan)",
				MinPointShapeDiameter = 10,
				MaxPointShapeDiameter = 60,
				PointGeometry = DefaultGeometries.Triangle,
				Fill = Brushes.DarkOrchid
			};
			ScatterSeries Science_fiction = new ScatterSeries
			{
				Title = "Science fiction (With public scan)",
				MinPointShapeDiameter = 10,
				MaxPointShapeDiameter = 60,
				PointGeometry = DefaultGeometries.Circle,
				Fill = Brushes.Red
			};
			ScatterSeries NotScience_fiction = new ScatterSeries
			{
				Title = "Science fiction (Without public scan)",
				MinPointShapeDiameter = 10,
				MaxPointShapeDiameter = 60,
				PointGeometry = DefaultGeometries.Triangle,
				Fill = Brushes.Red
			};
		public Form1()
		{
			InitializeComponent();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void label1_Click_1(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void Form2_Load(object sender, EventArgs e)
		{
			// TODO: данная строка кода позволяет загрузить данные в таблицу "testDataBaseDataSet12.ReadBooks". При необходимости она может быть перемещена или удалена.
			this.readBooksTableAdapter.Fill(this.testDataBaseDataSet12.ReadBooks);
		

			sqlConnection = new SqlConnection
				(ConfigurationManager.ConnectionStrings["TestDataBase"].ConnectionString);
			sqlConnection.Open();
			cartesianChart1.LegendLocation = LegendLocation.Right;
			
			cartesianChart1.DefaultLegend.Foreground = Brushes.Black;
			cartesianChart1.Zoom = ZoomingOptions.X;
			cartesianChart1.Pan = PanningOptions.None;
		
			cartesianChart1.ForeColor = System.Drawing.Color.Black;
			cartesianChart1.DataClick += CartesianChart1_DataClick;
			cartesianChart1.DataHover += CartesianChart1_DataHover;
			
		/*	this.dataGridView1.DataBindings["ReadBooks"].DataSourceUpdateMode =
	DataSourceUpdateMode.OnPropertyChanged;
*/

		}

		private void CartesianChart1_DataHover(object sender, ChartPoint chartPoint)
		{
			string tempAuthor="";
			string tempGenre = "";
			bool tempPublic_scan=false;

			foreach (var author in testDataBaseDataSet12.ReadBooks)
			{
				if (chartPoint.X==author.first_publish_year_ && chartPoint.Y==author.edition_count&&chartPoint.Weight==author.number_of_pages_median_)
				{
					tempAuthor =  author.author_name;
					tempGenre = author.category;
					tempPublic_scan = author.public_scan_b;
				}
			}
			ChartValues<ScatterPoint> authorsForConnectionns = new ChartValues<ScatterPoint>();


				LineSeries lineSeries = new LineSeries();

				foreach (var book in testDataBaseDataSet12.ReadBooks)
				{
					
					if (book.author_name == tempAuthor )
					{

						authorsForConnectionns.Add(new ScatterPoint(book.first_publish_year_, book.edition_count, book.number_of_pages_median_));


					}


				}
				
				if (authorsForConnectionns.Count >= 2 && list_with_autor_connections.Contains(tempAuthor)==false)
				{

					ChartValues<ScatterPoint> val = new ChartValues<ScatterPoint>();
					if (tempGenre.Equals("Fantasy"))
					{
				

						val.AddRange(fantasy);
						val.AddRange(Nfantasy);
						

					}
					
					else if (tempGenre.Equals("Programming"))
					{
						val.AddRange(programming);
						val.AddRange(Nprogramminng);

					}
					
					else if (tempPublic_scan == true && tempGenre.Equals("Science"))
					{

					val.AddRange(science);
					val.AddRange(Nscience);

					}
					
					else if (tempPublic_scan == true && tempGenre.Equals("Classical Literature"))
					{

					val.AddRange(classical_literature);
					val.AddRange(Nclassical_literature);


					}
					
		
					else if (tempPublic_scan == true && tempGenre.Equals("Science Fiction"))
					{

					val.AddRange(science_fiction);
					val.AddRange(Nscience_fiction);


					}
					

					foreach (var book in val)
					{
						int z = 0;
						foreach (var target in authorsForConnectionns)
						{
							if (book.X == target.X && book.Y == target.Y && book.Weight == target.Weight)
							{
								z++;
							}
						}
						if (z == 0)
						{
							val.Remove(book);
						}
						
					}
				//IEnumerable<ScatterPoint> valsorted = val.OrderBy(book => book.X);
				

				/*while (val.Count == 0)
				{
					foreach (var i in val)
					{
						if (val.Min().X == i.X)
						{
							
							lineSeries.Values.Add(i);
							val.Remove(i);
						}
					}
				}*/
				

				lineSeries.Values = val;
			
				lineSeries.Title = tempAuthor;
					lineSeries.Fill= Brushes.Transparent;

				if (series.Count>10)
				{
					Console.WriteLine("Szopaa");
				}
				series.Add(lineSeries);
				list_with_autor_connections.Add(tempAuthor);
				}

				authorsForConnectionns.Clear();

			
		}

		private void CartesianChart1_DataClick(object sender, ChartPoint p)
		{
		
			foreach (var i in testDataBaseDataSet12.ReadBooks)
			{
				if (i.edition_count == p.Y && i.first_publish_year_ == p.X)
				{
					MessageBox.Show("Book : "+ i.title + "\n" + "Author : " + i.author_name + "\n" + "Number of Pages: " + i.number_of_pages_median_);

					//cartesianChart1.Pan = PanningOptions.None;
				
				}
			}
		}

		private void tabPage1_Click(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged_1(object sender, EventArgs e)
		{

		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			String autor = $"{ textBox1.Text }";
			String title = $"{ textBox2.Text }";
			if (autor==null)
			{
				MessageBox.Show("Write the autor of the Book");
			} else if (title==null)
			{
				MessageBox.Show("Write the title of the Book");
			}
			else
			{

				String s = ($"title ={ textBox1.Text}&autor =+{textBox2.Text}");
				temporaryBook = APIreciver.ApiRequest("http://openlibrary.org/search.json?title=" + title + "&autor=" + autor + "&availability&limit=1&language:eng");
				if (temporaryBook.numFound == 0)
				{
					MessageBox.Show("Cant find Book");
				} 
				else
				{
					MessageBox.Show("Book Found");
					label14.Text = temporaryBook.docs[0].author_name[0];
					label15.Text = temporaryBook.docs[0].title;
					label20.Text = temporaryBook.docs[0].edition_count.ToString();
					label17.Text = temporaryBook.docs[0].first_publish_year.ToString();
					label18.Text = temporaryBook.docs[0].number_of_pages_median.ToString();
					label19.Text = temporaryBook.docs[0].public_scan_b.ToString();
					Console.WriteLine(temporaryBook.docs[0].author_name[0].Split(' ').Length + "---------------------------------------");
				}
				
			}

		}

		private void progressBar1_Click(object sender, EventArgs e)
		{

		}

		private void label4_Click(object sender, EventArgs e)
		{

		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (temporaryBook==null)
			{

				MessageBox.Show("First, find a book in the library");
			}
			else if (comboBox1.SelectedItem==null)
			{
				MessageBox.Show("Select a Category !!!!");
			}
			else
			{
				readBooksTableAdapter.Insert(temporaryBook.docs[0].title, temporaryBook.docs[0].author_name[0], temporaryBook.docs[0].edition_count,
			temporaryBook.docs[0].first_publish_year, temporaryBook.docs[0].number_of_pages_median, temporaryBook.docs[0].public_scan_b, comboBox1.SelectedItem.ToString());
				this.readBooksTableAdapter.Update(this.testDataBaseDataSet12.ReadBooks);
				SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [ReadBooks] (title,author_name,edition_count,first_publish_year,number_of_pages_median,public_scan_b,category) VALUES(@title,@author_name,@edition_count,@first_publish_year,@number_of_pages_median,@public_scan_b,@category)", sqlConnection);

				sqlCommand.Parameters.AddWithValue("title", temporaryBook.docs[0].title);
				sqlCommand.Parameters.AddWithValue("author_name", temporaryBook.docs[0].author_name[0]);
				sqlCommand.Parameters.AddWithValue("edition_count", temporaryBook.docs[0].edition_count);
				sqlCommand.Parameters.AddWithValue("first_publish_year", temporaryBook.docs[0].first_publish_year);
				sqlCommand.Parameters.AddWithValue("number_of_pages_median", temporaryBook.docs[0].number_of_pages_median);
				sqlCommand.Parameters.AddWithValue("public_scan_b", temporaryBook.docs[0].public_scan_b);
				sqlCommand.Parameters.AddWithValue("category", comboBox1.SelectedItem);
				sqlCommand.ExecuteNonQuery();
				MessageBox.Show("Book is added to library");
			}
			
		}



		private void button2_Click(object sender, EventArgs e)
		{
			list_with_autor_connections.Clear();
			foreach (var row in testDataBaseDataSet12.ReadBooks)
			{
				if (row.public_scan_b == true && row.category == comboBox1.Items[0].ToString())
				{
					programming.Add(new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));
					

				}
				else if (row.public_scan_b == false && row.category == comboBox1.Items[0].ToString())
				{
					Nprogramminng.Add(new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));

				}
				else if (row.public_scan_b == true && row.category == comboBox1.Items[3].ToString())
				{
					fantasy.Add(new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));
				}
				else if (row.public_scan_b == false && row.category == comboBox1.Items[3].ToString())
				{
					Nfantasy.Add(new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));
				}
				else if (row.public_scan_b == true && row.category == comboBox1.Items[1].ToString())
				{
					science.Add(new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));
				}
				else if (row.public_scan_b == false && row.category == comboBox1.Items[1].ToString())
				{
					Nscience.Add(new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));
				}
				else if (row.public_scan_b == true && row.category == comboBox1.Items[2].ToString())
				{
					classical_literature.Add(new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));
				}
				else if (row.public_scan_b == false && row.category == comboBox1.Items[2].ToString())
				{
					Nclassical_literature.Add(
						new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));
				
				}
				else if (row.public_scan_b == true && row.category == comboBox1.Items[4].ToString())
				{
					science_fiction.Add(new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));
				}
				else if (row.public_scan_b == false && row.category == comboBox1.Items[4].ToString())
				{
					Nscience_fiction.Add(new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));
				}
			}
			cartesianChart1.AxisX.Clear();
			cartesianChart1.AxisX.Add(new Axis()
			{
				Foreground=Brushes.Black,
				Title = "FIRST PUBLISH YEAR",
			
				FontSize = 15,
				MinValue = 1850,
				MaxValue = 2021
			}); ;
			cartesianChart1.AxisY.Clear();
			cartesianChart1.AxisY.Add(new Axis()
			{
				Foreground = Brushes.Black,
				Title = "NUMBER OF EDITIONS",
				FontSize = 15,
				MinValue = 0,
				MaxValue = 350
			});
			Programming.Values = programming;
			NotProgarmming.Values = Nprogramminng;
			Science.Values = science;
			NotScience.Values = Nscience;
			Classical_literature.Values = classical_literature;
			NotClassical_literature.Values = Nclassical_literature;
			Fantasy.Values = fantasy;
			NotFantasy.Values = Nfantasy;
			Science_fiction.Values = science_fiction;
			NotScience_fiction.Values = Nscience_fiction;
			/*LineSeries spotLine = new LineSeries();
			spotLine.Title = "Zopa";
			spotLine.Values = Nclassical_literature;
			series.Add(spotLine);*/
		
			series = new SeriesCollection {Science,NotScience,Fantasy,NotFantasy,Classical_literature,NotClassical_literature,
			Science_fiction,NotScience_fiction,Programming,NotProgarmming};
			
			cartesianChart1.Series = series;
		
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void button4_Click(object sender, EventArgs e)
		{
			

		}

		private void button4_Click_1(object sender, EventArgs e)
		{
			try
			{
				
				this.Validate();
				this.readBooksBindingSource3.EndEdit();
				this.readBooksTableAdapter.Update(this.testDataBaseDataSet12.ReadBooks);
				this.readBooksTableAdapter.Fill(this.testDataBaseDataSet12.ReadBooks);
				
				
				/*dataGridView1.DataBindings.Add(this, testDataBaseDataSet2, DataSourceUpdateMode.OnPropertyChanged);*/
				MessageBox.Show("Update successful");
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("Update failed");
			}
		}
		
		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
		{

		}

		private void readBooksBindingSource2_CurrentChanged(object sender, EventArgs e)
		{

		}

		private void tabPage2_Click(object sender, EventArgs e)
		{

		}

		private void label5_Click(object sender, EventArgs e)
		{

		}

		private void label7_Click(object sender, EventArgs e)
		{

		}

		private void label11_Click(object sender, EventArgs e)
		{

		}

		private void label12_Click(object sender, EventArgs e)
		{

		}

		private void label13_Click(object sender, EventArgs e)
		{

		}

		private void label19_Click(object sender, EventArgs e)
		{

		}

		private void label14_Click(object sender, EventArgs e)
		{

		}

		private void label15_Click(object sender, EventArgs e)
		{

		}

		private void label20_Click(object sender, EventArgs e)
		{

		}

		private void label17_Click(object sender, EventArgs e)
		{

		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void tabPage3_Click(object sender, EventArgs e)
		{

		}

		private void button4_Click_2(object sender, EventArgs e)
		{
			
		}

		private void button4_Click_3(object sender, EventArgs e)
		{
			
			/*foreach( var ser in series)
			{
				if (ser.GetType().FullName.Contains("lineSeries")){

					series.Remove(ser);
				}
			}


			ChartValues<ScatterPoint> autors = new ChartValues<ScatterPoint>();
			
			foreach (var row in testDataBaseDataSet12.ReadBooks)
			{
				LineSeries lineSeries = new LineSeries();
				foreach (var book in testDataBaseDataSet12.ReadBooks)
				{
					if (row.Id == book.Id)
					{
						continue;
					}
					if (row.author_name == book.author_name)
					{

						autors.Add(new ScatterPoint(book.first_publish_year_, book.edition_count, book.number_of_pages_median_));


					}


				}

				if (autors.Count != 0 && list_with_autor_connections.Contains(row.author_name) == false)
				{

					list_with_autor_connections.Add(row.author_name);
					autors.Add(new ScatterPoint(row.first_publish_year_, row.edition_count, row.number_of_pages_median_));
					ChartValues<ScatterPoint> val = new ChartValues<ScatterPoint>();
					if (row.public_scan_b == true && row.category.Equals("Fantasy"))
					{
						lineSeries.Values = fantasy;
						
					}
					else if (row.public_scan_b == false && row.category.Equals("Fantasy"))
					{
						//MessageBox.Show("opaaa");
						val = Nfantasy;
						lineSeries.Values = val;

					}
					else if (row.public_scan_b == true && row.category.Equals("Programming"))
					{
						
						val = programming;
						lineSeries.Values = val;

					}
					else if (row.public_scan_b == false && row.category.Equals("Programming"))
					{
						
						val = Nprogramminng;
						lineSeries.Values = val;

					}
					else if (row.public_scan_b == true && row.category.Equals("Science"))
					{

						val = science;
						lineSeries.Values = val;

					}
					else if (row.public_scan_b == false && row.category.Equals("Science"))
					{

						val = Nscience;
						lineSeries.Values = val;

					}
					else if (row.public_scan_b == true && row.category.Equals("Classical Literature"))
					{

						val = classical_literature;
						lineSeries.Values = val;

					}
					else if (row.public_scan_b == false && row.category.Equals("Classical Literature"))
					{

						val = Nclassical_literature;
						lineSeries.Values = val;

					}
					else if (row.public_scan_b == true && row.category.Equals("Science Fiction"))
					{

						val = science_fiction;
						lineSeries.Values = val;

					}
					else if (row.public_scan_b == false && row.category.Equals("Science Fiction"))
					{

						val = Nscience_fiction;
						lineSeries.Values = val;

					}

					foreach (var book in val)
					{
						int z = 0;
						foreach (var target in autors)
						{
							if (book.X == target.X && book.Y == target.Y && book.Weight == target.Weight)
							{
								z++;
							}
						}
						if (z == 0)
						{
							val.Remove(book);
						}

					}
					lineSeries.Title = row.author_name;
					lineSeries.Fill = Brushes.Transparent;
					series.Add(lineSeries);

				}

				autors.Clear();

			}*/
			
		}

		private void label7_Click_1(object sender, EventArgs e)
		{

		}

		private void label18_Click(object sender, EventArgs e)
		{

		}

		private void label16_Click(object sender, EventArgs e)
		{

		}

		private void label21_Click(object sender, EventArgs e)
		{

		}

		private void label22_Click(object sender, EventArgs e)
		{

		}
	}

}
