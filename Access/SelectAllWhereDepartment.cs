using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Access
{
    public partial class SelectAllWhereDepartment : Form
    {
        private OleDbConnection connection = new OleDbConnection();    // データベース接続用オブジェクト
        private OleDbDataAdapter dataAdapter = new OleDbDataAdapter(); // テーブル操作実行用オブジェクト
        private OleDbCommand command = new OleDbCommand();             // クエリ格納用オブジェクト

        private DataTable dataTable = new DataTable();

        public SelectAllWhereDepartment()
        {
            InitializeComponent();
            InitDB();
        }

        private void InitDB()
        {
            // データベースをオープン
            connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Data\\Database1.accdb";
            connection.Open();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 直前の取得結果をクリア
            dataGridViewEmployees.Rows.Clear();
            dataTable.Clear();

            string sql = "SELECT * FROM Employees WHERE Department = @Department";
            command.CommandText = sql;
            command.Connection = connection;

            // SQLインジェクション対策
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Department", txtDepartment.Text);

            dataAdapter.SelectCommand = command;

            // 実行
            dataAdapter.Fill(dataTable);

            // 取得結果表示
            foreach (DataRow dr in dataTable.Rows)
            {
                dataGridViewEmployees.Rows.Add(dr.ItemArray[0], dr.ItemArray[1], dr.ItemArray[2], dr.ItemArray[3]);
            }
            
        }      

        private void SelectAllWhereDepartment_FormClosing(object sender, FormClosingEventArgs e)
        {
            // データベースをクローズ
            connection.Close();
        }
    }
}
