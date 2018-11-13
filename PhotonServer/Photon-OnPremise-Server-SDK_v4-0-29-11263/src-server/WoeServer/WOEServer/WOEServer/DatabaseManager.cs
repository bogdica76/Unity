using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace WOEServer
{
    class DatabaseManager
    {

        private string ConnectionString()
        {
            return "server=thoe.ro;port=3306;userid=thoero_woe_game;password=woe1!2@3#;database=thoero_woe;";
        }

        public bool ConnectToDB()
        {
            string err = "";
            MySqlConnection conn = new MySqlConnection(ConnectionString());
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from users";
            try { conn.Open(); return true; } catch (Exception ex) { err = ex.Message; return false; }
        }

        public bool LoginToGame(string aUser, string aPass)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString());
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from users where username = '" + aUser + "' and password = '" + aPass + "'";
            try {
                conn.Open();
                //return true;
            }
            catch (Exception ex) {
                return false;
            }
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
