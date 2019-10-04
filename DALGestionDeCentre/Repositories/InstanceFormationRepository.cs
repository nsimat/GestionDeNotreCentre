﻿using GestionDeCentreDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.DataAccess;
using ToolBox.Repositories;

namespace GestionDeCentreDAL.Repositories
{
    class InstanceFormationRepository : IRepository<InstanceFormation, int>
    {
        private readonly Connection _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
            ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteAInstanceFormation");
            command.AddParameter("IdInstanceFormation", id);
            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public IEnumerable<InstanceFormation> Get()
        {
            Command command = new Command("SELECT * FROM V_InstanceFormation;");
            return _Connection.ExecuteReader(command, dr => new InstanceFormation().From(dr));
        }

        public InstanceFormation Get(int id)
        {
            Command command = new Command("SELECT * FROM V_InstanceFormation WHERE IdInstanceFormation = @IdInstanceFormation");
            command.AddParameter("IdInstanceFormation", id);
            return _Connection.ExecuteReader(command, dr => new InstanceFormation().From(dr)).FirstOrDefault();
        }

        public InstanceFormation Insert(InstanceFormation entity)
        {
            Command command = new Command("sp_InsertAInstanceFormation", true);
            command["Statut"] = entity.Statut;
            command["DateDebut"] = entity.DateDebut;
            command["DateFin"] = entity.DateFin;
            command["IdFormation"] = entity.Formation;
            command["IdPersonne"] = entity.Employe;
            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdInstanceformation = lastId;
            return entity;

        }

        public bool Put(InstanceFormation entity, int id)
        {
            Command command = new Command("sp_UpdateAInstanceFormation");
            return _Connection.ExecuteNonQuery(command) == 1;
        }

       

    }
}
