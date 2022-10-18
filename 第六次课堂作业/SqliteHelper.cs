using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace SchoolSystem
{

	
	public class SQLiteHelper
	{
		private static string connectionString = string.Empty;

		//根据数据源设置连接字符串。
		public static void SetConnectionString(string datasource)
		{
			connectionString = string.Format("Data Source={0}", datasource);
		}

		//创建一个数据库文件。如果存在同名数据库文件，则会覆盖。
		public static void CreateDB(string dbName)
		{
			if (!string.IsNullOrEmpty(dbName))
			{
				try 
				{ 
					SQLiteConnection.CreateFile(dbName);
					//创建数据库时清空log表
					System.IO.File.WriteAllText(@"E:\\LogFile.txt", string.Empty);
					string str = System.DateTime.Now.ToString() + "  创建数据库 "+dbName+" 成功！";
					writeLog(str);
				}
				catch (Exception) { throw; }
			}
		}

		//写入记录
		public static void writeLog(string str)
		{
			File.AppendAllText(@"E:\\LogFile.txt", str + Environment.NewLine);
		}

		public static void InitDB()
		{
			// 连接数据库    
			SQLiteConnection connection = new SQLiteConnection(connectionString);
			connection.Open();

			//创建student表
			string[] stColNames = new string[] { "ID", "Name", "belong", "Tel" };
			string[] stColTypes = new string[] { "INTEGER PRIMARY KEY", "TEXT", "INTEGER", "INTEGER" };
			string stTableName = "table3";
			string stQueryString = "CREATE TABLE IF NOT EXISTS " + stTableName + "( " + stColNames[0] + " " + stColTypes[0];

			for (int i = 1; i < stColNames.Length; i++)
			{
				stQueryString += ", " + stColNames[i] + " " + stColTypes[i];
			}
			stQueryString += "  ) ";
			SQLiteCommand stDBCommand = connection.CreateCommand();
			stDBCommand.CommandText = stQueryString;
			stDBCommand.ExecuteNonQuery();

			//创建class表
			string[] clColNames = new string[] { "ID", "Name", "belong", "headTeacher" };
			string[] clColTypes = new string[] { "INTEGER PRIMARY KEY", "TEXT", "INTEGER", "TEXT" };
			string clTableName = "table2";
			string clQueryString = "CREATE TABLE IF NOT EXISTS " + clTableName + "( " + clColNames[0] + " " + clColTypes[0];

			for (int i = 1; i < clColNames.Length; i++)
			{
				clQueryString += ", " + clColNames[i] + " " + clColTypes[i];
			}
			clQueryString += "  ) ";
			SQLiteCommand clDBCommand = connection.CreateCommand();
			clDBCommand.CommandText = clQueryString;
			clDBCommand.ExecuteNonQuery();

			//创建school表
			string[] scColNames = new string[] { "ID", "Name", "clNum", "headMaster" };
			string[] scColTypes = new string[] { "INTEGER PRIMARY KEY", "TEXT", "INTEGER", "TEXT" };
			string scTableName = "table1";
			string scQueryString = "CREATE TABLE IF NOT EXISTS " + scTableName + "( " + scColNames[0] + " " + scColTypes[0];

			for (int i = 1; i < scColNames.Length; i++)
			{
				scQueryString += ", " + scColNames[i] + " " + scColTypes[i];
			}
			scQueryString += "  ) ";
			SQLiteCommand scDBCommand = connection.CreateCommand();
			scDBCommand.CommandText = scQueryString;
			scDBCommand.ExecuteNonQuery();

			string str = System.DateTime.Now.ToString() + "  初始化数据库成功！";
			writeLog(str);
		}


		//删除表
		public static void dropTable()
		{
			// 连接数据库    
			SQLiteConnection connection = new SQLiteConnection(connectionString);
			if (connection.State != System.Data.ConnectionState.Open)
			{
				connection.Open();
				SQLiteCommand command = new SQLiteCommand();
				command.Connection = connection;
				command.CommandText = "DROP TABLE IF EXISTS table1";
				command.ExecuteNonQuery();//执行
				command.CommandText = "DROP TABLE IF EXISTS table2";
				command.ExecuteNonQuery();//执行
				command.CommandText = "DROP TABLE IF EXISTS table3";
				command.ExecuteNonQuery();//执行
			}
			connection.Close();

			string str = System.DateTime.Now.ToString() + "  所有数据已删除！";
			writeLog(str);
		}


		//对SQLite数据库执行增删改操作，返回受影响的行数。 
		public static int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
		{
			int affectedRows = 0;
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				using (SQLiteCommand command = new SQLiteCommand(connection))
				{
					try
					{
						connection.Open();
						command.CommandText = sql;
						if (parameters.Length != 0)
						{
							command.Parameters.AddRange(parameters);
						}
						affectedRows = command.ExecuteNonQuery();
					}
					catch (Exception) { throw; }
				}
			}
			return affectedRows;
		}



		//执行一个查询语句，返回一个包含查询结果的DataTable
		public static DataTable ExecuteQuery(string sql, params SQLiteParameter[] parameters)
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				using (SQLiteCommand command = new SQLiteCommand(sql, connection))
				{
					if (parameters.Length != 0)
					{
						command.Parameters.AddRange(parameters);
					}
					SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
					DataTable data = new DataTable();
					try { adapter.Fill(data); }
					catch (Exception) { throw; }
					return data;
				}
			}
		}

		public static DataTable ExecuteQuery(string sql)
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				using (SQLiteCommand command = new SQLiteCommand(sql, connection))
				{
					SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
					DataTable data = new DataTable();
					try { adapter.Fill(data); }
					catch (Exception) { throw; }
					return data;
				}
			}
		}

		//设置默认数据
		public static void inputData()
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				using (SQLiteCommand command = new SQLiteCommand(connection))
				{
					connection.Open();
					// 插入数据
					command.CommandText = " INSERT INTO [table1] (ID, Name, clNum, headMaster) VALUES (1,'school_1',3,'wang')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table1] (ID, Name, clNum, headMaster) VALUES (2,'school_2',2,'zhang')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table1] (ID, Name, clNum, headMaster) VALUES (3,'school_3',1,'li') ";
					command.ExecuteNonQuery();

					command.CommandText = " INSERT INTO [table2] (ID, Name, belong, headTeacher) VALUES (1,'class_1_1',1,'wang')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table2] (ID, Name, belong, headTeacher) VALUES (2,'class_1_2',1,'zhang')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table2] (ID, Name, belong, headTeacher) VALUES (3,'class_1_3',1,'li')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table2] (ID, Name, belong, headTeacher) VALUES (4,'class_2_1',2,'wang')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table2] (ID, Name, belong, headTeacher) VALUES (5,'class_2_2',2,'zhang')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table2] (ID, Name, belong, headTeacher) VALUES (6,'class_3_1',3,'li')";
					command.ExecuteNonQuery();

					command.CommandText = " INSERT INTO [table3] (ID, Name, belong, Tel) VALUES (1,'student_1',1,'wang')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table3] (ID, Name, belong, Tel) VALUES (2,'student_2',2,'zhang')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table3] (ID, Name, belong, Tel) VALUES (3,'student_3',3,'li')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table3] (ID, Name, belong, Tel) VALUES (4,'student_4',4,'wang')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table3] (ID, Name, belong, Tel) VALUES (5,'student_5',5,'zhang')";
					command.ExecuteNonQuery();
					command.CommandText = " INSERT INTO [table3] (ID, Name, belong, Tel) VALUES (6,'student_6',6,'li')";
					command.ExecuteNonQuery();

					string str = System.DateTime.Now.ToString() + "  数据导入成功！";
					writeLog(str);
				}
			}
		}
	}

}
