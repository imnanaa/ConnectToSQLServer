using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Controls;

namespace ConnectToSQLServer
{
    public partial class MainWindow : Window
    {
        string connectionString = null;        
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MessageBox.Show(connectionString);
            GetData1();
            GetData2();
            GetData3();
            GetData4();
            GetData5();
            GetData6();
            GetData7();
            GetData8();
        }

        private void GetAndDhowData(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGrid.ItemsSource = Table.DefaultView;
            connection.Close();            
        }

        private void GetData1()
        {
            string sqlQ = @"SELECT
s.Name AS [назва пісні],
s.Composer AS [композитор],
s.Author AS [автор],
s.Date AS[дата створення],
g.Name AS [назва групи]
FROM 
Song s
INNER JOIN Groups g ON s.GroupID= g.ID
WHERE 
s.Name ='Black Swan'";
            try
            {
                GetAndDhowData(sqlQ, T1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetData2()
        {
            string sqlQ = @"SELECT 
s.Name AS [назва пісні],
g.Name AS [група]
FROM 
Groups g
 INNER JOIN Song s ON g.ID=s.GroupID
 WHERE
 g.Position='1'";
            try
            {
                GetAndDhowData(sqlQ, T2);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetData3()
        {
            string sqlQ = @"SELECT TOP 1
tt.Price AS [Ціна квитка],
g.Name AS [Група]
FROM 
Groups g 
INNER JOIN Tour t ON g.TourID=t.ID
INNER JOIN TourToCity tt ON tt.TourID=t.ID
WHERE 
g.Name='Stray Kids'
ORDER BY 
tt.StartTime DESC";
            try
            {
                GetAndDhowData(sqlQ, T3);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetData4()
        {
            string sqlQ = @"SELECT 
m.Name AS [Виконавець],
m.Age AS[Вік],
m.Type AS [Амплуа],
g.Name AS[Група]
FROM 
Member m
INNER JOIN Groups g ON m.GroupID=g.ID
WHERE 
g.Name='BLACKPINK'";
            try
            {
                GetAndDhowData(sqlQ, T4);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetData5()
        {
            string sqlQ = @"SELECT 
c.Name AS [Місто],
tt.StartTime AS [Початок концерту],
tt.EndTime AS [Кінець концерту]
FROM 
TourToCity tt
INNER JOIN City c ON tt.CityID=c.ID
INNER JOIN Tour t ON tt.TourID=t.ID
INNER JOIN Groups g ON t.GroupID= g.ID
WHERE 
g.Name='Stray Kids'
";
            try
            {
                GetAndDhowData(sqlQ, T5);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetData6()
        {
            string sqlQ = @"SELECT 
g.Name AS [Групи, які празнують ювілей]
FROM 
Groups g
WHERE 
g.Year LIKE '___%2%' AND g.Year NOT LIKE '__%22%'";
            try
            {
                GetAndDhowData(sqlQ, T6);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetData7()
        {
            string sqlQ = @"SELECT TOP 1
m.Name AS [Виконавець],
m.Age AS[Вік],
g.Name AS[Група]
FROM 
Member m
INNER JOIN Groups g ON m.GroupID=g.ID
WHERE 
m.Type='Vocalist'
ORDER BY 
m.Age
";
            try
            {
                GetAndDhowData(sqlQ, T7);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetData8()
        {
            string sqlQ = @"SELECT 
g.Name AS[Група],
AVG(m.Age) AS [Середній вік]
FROM 
Member m
INNER JOIN Groups g ON m.GroupID=g.ID
GROUP BY 
g.Name
 HAVING  
AVG(m.Age)<=20";
            try
            {
                GetAndDhowData(sqlQ, T8);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}