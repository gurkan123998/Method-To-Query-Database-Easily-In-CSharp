using System.Data.SqlClient; // This namespace is required to use methods of SqlClient

/*
* This is not the complete class itself. Just use this stub/method based on your need to query databases.
*/

private void QueryDatabase(string SQLConnectionString, int SQLTimeout = 120)
{
	SqlConnection conn = new SqlConnection(SQLConnectionString);
  SqlCommand comm = null;
	
	DataTable DT = new DataTable();
	
	try
            {
                try
                {
                    comm = conn.CreateCommand();
                    comm.CommandType = CommandType.Text; // You can use CommandType.StoredProcedure instead as well
                    comm.CommandText = "#YOUR QUERY GOES HERE#";
                    comm.CommandTimeout = SQLTimeout;
		    /*
		    * // Uncomment the following line if you need to send in Sql parameters in your query. You can pass
		    * // different kind of paramters based on the field type in the database table.
		    * // For more info visit: https://msdn.microsoft.com/en-us/library/system.data.sqldbtype(v=vs.110).aspx
                    * comm.Parameters.Add(new SqlParameter("@FieldName", SqlDbType.VarChar, 100)).Value = yourPassedParameter;
		    */
                    conn.Open(); // Always remember to open connection before executing the Query.
                    comm.ExecuteNonQuery(); // Comment this line and uncomment the lines below to retrieve data in DataTable
		    /*
		    * //Uncomment this part to fetch data from table in DataTable using SqlDataAdapter -->
		    * SqlDataAdapter adapter = new SqlDataAdapter(comm);
                    * adapter.Fill(DT);
		    */
                }
                catch (SqlException err)
                {
		    throw new CustomSqlException("Something went wrong....! ", err.Message);
                }
            }
            catch (Exception ex)
            {
		throw new Exception(ex.Message.ToSTring());
            }
            finally
            {
                conn.Close(); // Always remember to close connection to database or else it would lead to memory leakage
            }
}



/*
* Following is building your own custom SqlException to catch sql exceptions properly when they happen.
* This step is not necessary but good to have this extra step to process exceptions
* properly based on your needs.
*/

public class CustomSqlException : Exception
{
 	public CustomSqlException()
 	{
 	}

 	public CustomSqlException(string message, SqlException innerException) : base(message, innerException)
 	{
 	}
}
