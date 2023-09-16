using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml.Linq;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Windows;

namespace myChat
{
    internal class DBConnect
    {
        string my_query;
        SQLiteCommand myQuery;
        SQLiteConnection myConnection;
        SQLiteDataReader _dr;
        string DBName;
        public DBConnect(string _DBName)
        {
            my_query = @"CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                  @"Password INTEGER(50) UNIQUE NOT NULL ," +
                                  @"UserName TEXT(1, 50)," +
                                  @"LoginName TEXT(1, 50) UNIQUE NOT NULL," +
                                  @"position INTEGER(2));";
            DBName = _DBName;
            sqlConnect(my_query);
            my_query = @"CREATE TABLE IF NOT EXISTS Chats (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                            "name TEXT UNIQUE);";
            sqlConnect(my_query);
            my_query = @"CREATE TABLE IF NOT EXISTS messeges (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                                    @" user_id INTEGER REFERENCES Users (id),"+
                                                    @" chat_id INTEGER REFERENCES Chats (id),"+
                                                     @" messege TEXT,"+
                                                        @" time TIME);";
            sqlConnect(my_query);

        }
        private void sqlConnect(string _my_query)
        {
            string _source = "Data Source=" + DBName + ".db;";
            string _cache = "Cache=Shared;";
            string _mode = "Mode=ReadWriteCreate;";
            myConnection = new SQLiteConnection(_source + _cache + _mode);
            myConnection.Open();
            myQuery = new SQLiteCommand(_my_query, myConnection);
            try
            {
                _dr = myQuery.ExecuteReader();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public void AddUser(User name)
        {
            my_query = @"INSERT INTO Users(Password, LoginName, position) " +
                @"VALUES('" + name.Password + @"','" + name.LoginName + @"'," +name.isAdministration+");";
            sqlConnect(my_query);
        }
        public void messege(string messege,int user_id,int chat_id) {
            my_query = @"INSERT INTO messeges(user_id,chat_id, messege, time) " +
                @"VALUES(" + user_id + @"," + chat_id + @",'" + messege + @"','" + DateTime.Now.ToString("HH:mm:ss") + @"');";
            sqlConnect(my_query);
        }
        public List<Messege> messegeHistory() {

            List<Messege> list = new List<Messege>();

            my_query = @"Select UserName,messege,time "+
                        @"FROM messeges,Users " +
                        @"WHERE messeges.user_id == Users.id;";
            sqlConnect(my_query);
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    var user = _dr.GetString(0);
                    var mess = _dr.GetString(1);
                    var time = _dr.GetDateTime(2);
                    Messege messege = new Messege(mess,time,user);
                    list.Add(messege);
                }
            }
            return list;
        }
        private void updateAdministration(User user, int isAdmin) {
            user.isAdministration=0;
            my_query = @"UPDATE Users" +
                        @"SET position = " + isAdmin +
                        @"WHERE LoginName = '"+user.LoginName+ "' AND Password = "+user.Password +";";
            sqlConnect(my_query);
        }
        public void updateUserName(User user)
        {            
            my_query = @"UPDATE Users " +
                        @"SET UserName = '" + user.Name+ "' "+
                        @"WHERE LoginName = '" + user.LoginName + "' AND Password = " + user.Password + ";";
            sqlConnect(my_query);
        }
       
        public List<User> mychatLoad()
        {
            List<User> list = new List<User>();
            
            my_query = @"Select * From Users;";
            sqlConnect(my_query);
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    User user = new User();
                    user.Id = _dr.GetInt32(0);
                    user.Password = _dr.GetInt32(1);
                    user.Name = _dr.GetString(2);
                    user.LoginName = _dr.GetString(3);
                    user.isAdministration = _dr.GetInt32(4);
                    list.Add(user);
                }
            }
            return list;
        }
       public void AddUserID(User user)
        {
            my_query = @"Select id FROM Users " +
                        @"WHERE LoginName = '" + user.LoginName + "' AND Password = " + user.Password + ";";
            sqlConnect(my_query);
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    var id = _dr.GetInt32(0);
                    user.Id = id;
                }
            }
        }

    }
}
