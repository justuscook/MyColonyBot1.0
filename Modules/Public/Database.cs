using Discord;
using Discord.WebSocket;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using cloudscribe.HtmlAgilityPack;

namespace MyBot.Modules.Public
{
	public class Database
	{
		private string table { get; set;}
		private const string database = "xxxxxxxxxx";
		private const string username = "xxxxxxxxxx";
		private const string password = "xxxxxxxxxx";
		private MySqlConnection dbConnection;

		public Database(string table)
		{
			this.table = table;
			MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
			stringBuilder.Server = GetIP();
			stringBuilder.UserID = username;
			stringBuilder.Password = password;
			stringBuilder.Database = database;
			stringBuilder.SslMode = MySqlSslMode.None;

			var connectionString = stringBuilder.ToString();

			dbConnection = new MySqlConnection(connectionString);

			dbConnection.Open();
		}

		public MySqlDataReader FireCommand(string query)
		{
			if (dbConnection == null)
			{
				return null;
			}

			MySqlCommand command = new MySqlCommand(query, dbConnection);

			var mySqlReader = command.ExecuteReader();

			return mySqlReader;
		}

		public void CloseConnection()
		{
			if (dbConnection != null)
			{
				dbConnection.Close();
			}
		}

		public static string GetIP()
		{
            //this just changes the IP for database depending on where its running, its on the MySQL server machine its localhost, otherwise its the ip for the mysql
			HtmlWeb obj = new HtmlWeb();
			HtmlDocument ipget = obj.Load("http://www.whatismypublicip.com/");
			string myIP = ipget.DocumentNode.SelectSingleNode("//div[@id='up_finished']").InnerText.Trim();
			if (myIP == "xxxxxxxxxx")
			{
				myIP = "localhost";
			}
			else
			{
				myIP = "xxxxxxxxxx";
			}
			return myIP;
		}
		public static List<String> CheckExistingServer(ulong server)
		{
			var result = new List<String>();
			var database = new Database(Database.database);

			var str = string.Format("SELECT * FROM serverinfo WHERE server_id = '{0}'", server);
			var tableName = database.FireCommand(str);

			while (tableName.Read())
			{
				var serverID = (string)tableName["server_id"];

				result.Add(serverID);
			}

			return result;
		}
		public static List<String> CheckExistingUser(string user)
		{
			var result = new List<String>();
			var database = new Database(Database.database);

			var str = string.Format("SELECT * FROM userinfo WHERE user_id = '{0}'", user);
			var tableName = database.FireCommand(str);

			while (tableName.Read())
			{
				var userID = (string)tableName["user_id"];

				result.Add(userID);
			}

			return result;
		}

		public static List<String> CheckHiSwitch(ulong server)
		{
			var result = new List<String>();
			var database = new Database(Database.database);

			var str = string.Format("SELECT hi_switch FROM serverinfo WHERE server_id = '{0}'", server);
			var tableName = database.FireCommand(str);

			while (tableName.Read())
			{
				var hiswitch = (int)tableName["hi_switch"];

				result.Add(hiswitch.ToString());
			}

			return result;

		}
		public static List<String> CheckBeerMeSwitch(ulong server)
		{
			var result = new List<String>();
			var database = new Database(Database.database);

			var str = string.Format("SELECT beerme_switch FROM serverinfo WHERE server_id = '{0}'", server);
			var tableName = database.FireCommand(str);

			while (tableName.Read())
			{
				var beermeswitch = (int)tableName["beerme_switch"];

				result.Add(beermeswitch.ToString());
			}
			return result;

		}

		public static List<String> CheckMemeSwitch(ulong server)
		{
			var result = new List<String>();
			var database = new Database(Database.database);

			var str = string.Format("SELECT meme_switch FROM serverinfo WHERE server_id = '{0}'", server);
			var tableName = database.FireCommand(str);

			while (tableName.Read())
			{
				var memeswitch = (int)tableName["meme_switch"];

				result.Add(memeswitch.ToString());
			}
			return result;

		}

        public static List<String> CheckCharterCode(ulong userid)
        {
            var result = new List<String>();
            var database = new Database(Database.database);

            var str = string.Format("SELECT main_charter_code FROM userinfo WHERE user_id = '{0}'", userid);
            var tableName = database.FireCommand(str);

            while (tableName.Read())
            {
                var mainchatercode = (string)tableName["main_charter_code"];

                result.Add(mainchatercode.ToString());
            }
            return result;

        }

        public static string EnterServer(ulong server, string serverName)
        {
            var database = new Database(Database.database);

            var str = string.Format("INSERT INTO serverinfo (server_id, server_name) VALUES ('{0}',\"{1}\")", server, serverName);
            var table = database.FireCommand(str);

            database.CloseConnection();

            return null;
        }
		public static string EnterUser(ulong userid, string username)
		{
			var database = new Database(Database.database);
			var str = string.Format("INSERT INTO userinfo (user_id, username) VALUES ('{0}',\"{1}\")", userid, username);
			var table = database.FireCommand(str);

			database.CloseConnection();

			return null;
		}
		public static string ToggleHiSwitch(ulong server, string toggle)
        {
            var database = new Database(Database.database);
            var str = string.Format("UPDATE serverinfo SET hi_switch='{1}' WHERE server_id='{0}'",server, toggle);
			var table = database.FireCommand(str);

            database.CloseConnection();

            return null;
        }
		public static string ToggleBeerMeSwitch(ulong server, string toggle)
		{
			var database = new Database(Database.database);
			var str = string.Format("UPDATE serverinfo SET beerme_switch='{1}' WHERE server_id='{0}'", server, toggle);
			var table = database.FireCommand(str);

			database.CloseConnection();

			return null;
		}

		public static string ToggleMemeSwitch(ulong server, string toggle)
		{
			var database = new Database(Database.database);
			var str = string.Format("UPDATE serverinfo SET meme_switch='{1}' WHERE server_id='{0}'", server, toggle);
			var table = database.FireCommand(str);

			database.CloseConnection();

			return null;
		}
		public static string EnterCharterCode(string userid, string charter)
		{
			var database = new Database(Database.database);
			var str = string.Format("UPDATE userinfo SET main_charter_code='{1}' WHERE user_id='{0}'", userid, charter);
			var table = database.FireCommand(str);

			database.CloseConnection();

			return null;
		}
	}
}
