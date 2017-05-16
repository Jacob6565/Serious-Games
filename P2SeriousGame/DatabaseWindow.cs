using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace P2SeriousGame
{
    public partial class DatabaseWindow : Form
    {
        public DatabaseWindow()
        {
            InitializeComponent();


            

            
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["metadata = res://*/SQL.Entities.csdl|res://*/SQL.Entities.ssdl|res://*/SQL.Entities.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=p2-avengers.database.windows.net;initial catalog=p2-database;persist security info=True;user id=tuandrengen;password=Aouiaom17;MultipleActiveResultSets=True;App=EntityFramework&quot; providerName = System.Data.EntityClient"].ToString();

            /*
            connectionString = ConfigurationManager.ConnectionStrings["Data Source=p2-avengers.database.windows.net;" +
                "Initial Catalog=p2-database;" +
                "User ID=tuandrengen;" +
                "Password=Aouiaom17;"].ConnectionString;*/

            /*
            connection.ConnectionString =
                "Data Source=p2-avengers.database.windows.net;" +
                "Initial Catalog=p2-database;" +
                "User ID=tuandrengen;" +
                "Password=Aouiaom17;";*/


            /*
            // Hentet fra app.confiq, som vi lavede da vi tilføjede reference database... - Bruger til at kommunikerer med vores database
            connectionString = ConfigurationManager.ConnectionStrings["Data Source=p2-avengers.database.windows.net;" +
                "Initial Catalog=p2-database;" +
                //"Persist Security Info=True;" +
                "User ID=tuandrengen;" +
                "Password=Aouiaom17;"].ConnectionString; */
        }

        SqlConnection con = new SqlConnection();
        SqlConnection connection = new SqlConnection();
        string connectionString;

        private static DatabaseWindow _instance;
        public static DatabaseWindow GetInstance()
        {
            if (_instance == null) _instance = new DatabaseWindow();
            {
                return _instance;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string query = "SELECT * FROM Person";

            using (connection = new SqlConnection(con.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable testTable = new DataTable();
                adapter.Fill(testTable);
                this.dataGridView1.DataSource = testTable;


            }
        }
    }
}
