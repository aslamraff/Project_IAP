﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project_IAP.Context;
using Project_IAP.Models;
using Project_IAP.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Project_IAP.Repository.Data
{
    public class PlacementRepository : GeneralRepository<Placement, MyContext>
    {
        private readonly MyContext _myContext;
        DynamicParameters parameters = new DynamicParameters();
        IConfiguration _configuration { get; }
        public PlacementRepository(MyContext mycontexts, IConfiguration configuration) : base(mycontexts)
        {
            _configuration = configuration;
            _myContext = mycontexts;
        }

        public async Task<IEnumerable<PlacementVM>> DataHistory()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var spName = "SP_RetrieveAll_TB_T_Placement";
                var data = await connection.QueryAsync<PlacementVM>(spName, commandType: CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<IEnumerable<PlacementVM>> DataInterview()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var spName = "SP_RetrieveEmpInterview_TB_T_Placement";
                var data = await connection.QueryAsync<PlacementVM>(spName, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<IEnumerable<PlacementVM>> DataPlacement()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var spName = "SP_RetrieveEmpPlacement_TB_T_Placement";
                var data = await connection.QueryAsync<PlacementVM>(spName, commandType: CommandType.StoredProcedure);
                return data;
            }
        }

        //User
        public async Task<IEnumerable<PlacementVM>> DataUserHistory(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var spName = "SP_UserHistory_TB_T_Placement";
                parameters.Add("@UserId", id);
                var data = await connection.QueryAsync<PlacementVM>(spName, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
        public async Task<IEnumerable<PlacementVM>> DataUserPlacement(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var spName = "SP_UserPlacement_TB_T_Placement";
                parameters.Add("@UserId", id);
                var data = await connection.QueryAsync<PlacementVM>(spName, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
        //User

        public async Task<Placement> CancelPlacement(int id)
        {
            var entity = await Get(id);
            if (id != entity.Id)
            {
                return entity;
            }
            entity.Status = 3;
            _myContext.Entry(entity).State = EntityState.Modified;
            await _myContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Placement> AssignEmployee(Placement entity)
        {
            entity.Status = 0;
            await _myContext.Set<Placement>().AddAsync(entity);
            await _myContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Placement> ConfirmInterview(int id)
        {
            var entity = await Get(id);
            if (id != entity.Id)
            {
                return entity;
            }
            entity.Status = 1;
            _myContext.Entry(entity).State = EntityState.Modified;
            await _myContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Placement> ConfirmPlacement(int id, PlacementVM input)
        {
            var entity = await Get(id);
            if (id != entity.Id)
            {
                return entity;
            }
            entity.Status = 2;
            entity.StartContract = input.StartContract;
            entity.EndContract = input.EndContract;
            _myContext.Entry(entity).State = EntityState.Modified;
            await _myContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<PlacementVM>> SetWorkStatus(int id, int userid)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var spName = "SP_SetWorkStatus_TB_M_User";
                parameters.Add("@Set", id);
                parameters.Add("@UserId", userid);
                var data = await connection.QueryAsync<PlacementVM>(spName, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<IEnumerable<PlacementVM>> GetByStatus(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var spName = "SP_DataSendEmail_TB_T_Placement";
                parameters.Add("@Id", id);
                var data = await connection.QueryAsync<PlacementVM>(spName, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
    }
}