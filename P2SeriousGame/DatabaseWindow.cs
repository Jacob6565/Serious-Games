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
        }

        SqlConnection connection = new SqlConnection();

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
        {
            DataSource = "p2-avengers.database.windows.net",
            UserID = "tuandrengen",
            Password = "Aouiaom17",
            InitialCatalog = "p2-database"
        }; // https://docs.microsoft.com/en-us/azure/sql-database/sql-database-connect-query-dotnet

        private static DatabaseWindow _instance;
        public static DatabaseWindow GetInstance()
        {
            if (_instance == null) _instance = new DatabaseWindow();
            {
                return _instance;
            }
        }

        private void GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "p2-avengers.database.windows.net";
            builder.UserID = "tuandrengen";
            builder.Password = "Aouiaom17";
            builder.InitialCatalog = "p2-database";
        }

        private void PopulatePersons()
        {
            string query = "SELECT * FROM Person";

            using (connection = new SqlConnection(builder.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable personTable = new DataTable();
                adapter.Fill(personTable);

                listBox1.DisplayMember = "First Name";
                listBox1.ValueMember = "Id";
                listBox1.DataSource = personTable;
            }
        }

        private void PopulateDataGrid() // note to self - en listbox med selectedvalue sat til id bruges til foreign key ud fra det viser datagrid
        {
            string query = "SELECT * FROM Person";

            using (connection = new SqlConnection(builder.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                DataTable testTable = new DataTable();
                adapter.Fill(testTable);
                this.dataGridView1.DataSource = testTable;
            }
        }

        private void DatabaseWindow_Load(object sender, System.EventArgs e)
        {
            PopulatePersons();
            PopulateDataGrid();
        }
    }
}
