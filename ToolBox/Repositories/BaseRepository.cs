using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToolBox.DataAccess;

namespace ToolBox.Repositories
{
    /// <summary>
    /// Repository Generic permettant le CRUD de n'importe quel type D'objets
    /// </summary>
    /// <typeparam name="TResult">Classe des objets manipulé</typeparam>
    /// <typeparam name="Tkey">Type de la cle primaire</typeparam>
    public abstract class BaseRepository<TResult, Tkey> : IRepository<TResult, Tkey> where TResult : class where Tkey : struct
    {
        /// <summary>
        /// Cree la connection avec la Base de donnés
        /// </summary>
        /// <param name="connectionString">String de la connection </param>
        /// <param name="ClientDB">Choix :"SqlClient" , "OracleClient", "OdbcClient" ,"OleDbClient"</param>
        public abstract Connection connection { get; }
        /// <summary>
        /// Nom de la table 
        /// </summary>
        public abstract string TableName { get;}
        /// <summary>
        /// IDateRecord qui permet de lire les element de la table
        /// (Get())
        /// </summary>
        /// <param name="dr">IDataRecord</param>
        /// <returns></returns>
        public abstract TResult setBindings(IDataRecord dr);
        /// <summary>
        /// Nom des colonne de la table sans la clé primaire
        /// (Les propriete de la classe doivent etre dans le meme ordre et similaire a ceux de la table de la base de données )
        /// </summary>
        public abstract IEnumerable<String> ColumsName { get; }
        /// <summary>
        /// Cle primaire de la table
        /// </summary>
        public abstract Tkey PKName { get; }
        /// <summary>
        /// Vrai si la clé primaire de la table est Auto Incrementé
        /// </summary>
        public abstract bool AutoIncrement { get; }

        /// <summary>
        /// Retourne tout les element d'une table. 
        /// </summary>
        /// <returns>Elements de la table</returns>
        public IEnumerable<TResult> Get()
        {
            Command command = new Command("SELECT * FROM " + TableName);
            return connection.ExecuteReader(command, e => setBindings(e));
        }
        /// <summary>
        /// Retourne un element recherche de la base de données.
        /// </summary>
        /// <param name="id">Id de l'instance recherche</param>
        /// <returns>L'instance recherche</returns>
        public TResult Get(Tkey id)
        {
            Command command = new Command("SELECT * FROM " + TableName + " WHERE "+ PKName + " = @id "); //type info collum
            command["id"] = id;
            return (TResult)connection.ExecuteScalar(command);
        }
        /// <summary>
        /// Insere un instance dans la base de données.
        /// </summary>
        /// <param name="entity">Objet a injecter</param>
        /// <returns>Retourne l'objet</returns>
        public TResult Insert(TResult entity)
        {
            string query = GetQuery();
            Command command = new Command(query);
            AddParametter(command, entity);
            connection.ExecuteNonQuery(command);
            return entity;
            
            
        }
        /// <summary>
        /// Modifie un instance de la base de données. 
        /// </summary>
        /// <param name="entity"> Objet qui doit etre modifie</param>
        /// <param name="id">Id recherche (AUTO INCREMENT OBLIGE)</param>
        /// <returns>Vrai ou faux</returns>
        public bool Put(TResult entity, Tkey id)
        {
            string query = GetQuery(id);
            Command command = new Command(query);
            AddParametter(command, entity);
            return  connection.ExecuteNonQuery(command)==0?false : true;
            
        }
        /// <summary>
        /// Supprime une instance de la base de données
        /// </summary>
        /// <param name="id">Id Recherche</param>
        /// <returns>Vrai ou faux</returns>
        public bool Delete(Tkey id)
        {
            Command command = new Command("Delete FROM " + TableName + " WHERE " + PKName + " = @id "); //type info collum
            command["id"] = id;
            return connection.ExecuteNonQuery(command)==1?true :false;
        }
        /// <summary>
        /// Cree la query pour un insert 
        /// </summary>
        /// <returns>La query pour l'insert en string (Accepte Id non incrementé)</returns>
        private string GetQuery()
        {
            List<string> prop = new List<string>();
            prop = GetProperty();
            string query = "";
          
                    if (!AutoIncrement)
                    {
                        query = "INSERT INTO " + TableName + " VALUES(";
                        foreach (string item in prop)
                        {
                            if (prop.LastOrDefault().Equals(item))
                            {
                                query += "@" + item;
                            }
                            else
                            {
                                query += "@" + item + ",";
                            }

                        }
                        query += ");";

                    }
                    else
                    {
                        query = "INSERT INTO " + TableName + " VALUES(";
                        prop.RemoveAt(0);
                        foreach (string item in prop)
                        {
                            if (prop.LastOrDefault().Equals(item))
                            {
                                query += "@" + item;
                            }
                            else
                            {
                                query += "@" + item + ",";
                            }

                        }
                        query += ");";


                    }
                  
               

            return query;

        }
        /// <summary>
        /// Cree la query pour l'update
        /// </summary>
        /// <param name="id">Id recherche</param>
        /// <returns>La query pour l'update en string (Refuse Id non incrementé)</returns>
        private string GetQuery( Tkey id)
        {
            List<string> prop = new List<string>();
            prop = GetProperty();
            string query = "";

            query = "UPDATE " + TableName + " SET ";

            for (int i = 0; i < ColumsName.Count(); i++)
            {
                if (prop.LastOrDefault().Equals(ColumsName))
                {
                    query += ColumsName.ElementAt(i) + " = " + "@ " + prop[i];
                }

                else
                {
                    query += ColumsName.ElementAt(i) + " = " + "@" + prop[i] + " , ";
                }
            }

            query += "WHERE " + PKName + " = " + id + ";";


            return query;

        }
        /// <summary>
        /// Ajoute les parametre a la requette
        /// </summary>
        /// <param name="command">La query</param>
        /// <param name="entity">Objet qui va remplis les parametre </param>
        private void AddParametter(Command command , TResult entity)
        {
            Type f = typeof(TResult);
            PropertyInfo[] e = f.GetProperties();
            foreach (PropertyInfo item in e)
            {
                command.AddParameter(item.Name, item.GetValue(this,null));
            }
            
        }
        /// <summary>
        /// Cree la Liste de propriete d'une classe
        /// </summary>
        /// <returns>List<String> des propriete de la classe</returns>
        private List<string> GetProperty()
        {
            Type f = typeof(TResult);
            PropertyInfo[] e = f.GetProperties();
            List<string> prop = new List<string>();
            foreach (PropertyInfo item in e)
            {
               prop.Add((string)item.Name);
            }
            return prop;

        }
    }
}
