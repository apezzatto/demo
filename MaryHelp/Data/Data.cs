using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MaryHelp.DataLayer
{
    public class Data
    {
        private string _connectionString;
        private MySqlTransaction _transaction = null;
        private MySqlConnection _conn = null;

        public Data(string connectionString, bool beginTransaction = false)
        {
            _connectionString = connectionString;
            _conn = new MySqlConnection(_connectionString);
            _conn.Open();

            if (beginTransaction)
                _transaction = _conn.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public void CloseConnection()
        {
            if (_conn != null)
            { 
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
            _conn = null;
        }

        public void ExecuteNonQuery(string procedureName, List<MySqlParameter> parameters)
        {
            MySqlCommand command = null;

            try
            {
                command = _conn.CreateCommand();
                command.Transaction = _transaction;
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;

                foreach (MySqlParameter p in parameters)
                    command.Parameters.Add(p);

                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                throw new Exception("There is something wrong with the database: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("General exception: " + e.Message);
            }
            finally
            {
                command.Dispose();
                command = null;
            }
        }

        public string ExecuteScalar(string procedureName, List<MySqlParameter> parameters)
        {
            MySqlCommand command = null;
            MySqlDataAdapter adap = null;
            DataSet ds;
            string ret = null;

            try
            {
                command = _conn.CreateCommand();
                command.Transaction = _transaction;
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;

                foreach (MySqlParameter p in parameters)
                    command.Parameters.Add(p);

                adap = new MySqlDataAdapter(command);

                ds = new DataSet();

                adap.Fill(ds);

                if (ds.Tables[0].Rows.Count.Equals(1))
                    ret = ds.Tables[0].Rows[0][0].ToString();

                return ret;
            }
            catch (MySqlException e)
            {
                throw new Exception("There is something wrong with the database: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("General exception: " + e.Message);
            }
            finally
            {
                command.Dispose();
                adap.Dispose();
                command = null;
                adap = null;
            }
        }

        public DataSet ExecuteQuery(string procedureName, List<MySqlParameter> parameters = null)
        {
            MySqlCommand command = null;
            MySqlDataAdapter adap = null;
            DataSet ds;

            try
            {
                command = _conn.CreateCommand();
                command.Transaction = _transaction;
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (MySqlParameter p in parameters)
                        command.Parameters.Add(p);
                }

                adap = new MySqlDataAdapter(command);

                ds = new DataSet();

                adap.Fill(ds);

                return ds;
            }
            catch (MySqlException e)
            {
                throw new Exception("There is something wrong with the database: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("General exception: " + e.Message);
            }
            finally
            {
                command.Dispose();
                adap.Dispose();
                command = null;
                adap = null;
            }
        }
    }
}
