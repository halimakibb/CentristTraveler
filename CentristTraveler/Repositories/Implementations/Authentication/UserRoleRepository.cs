﻿using CentristTraveler.Models;
using CentristTraveler.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace CentristTraveler.Repositories.Implementations
{
    public class UserRoleRepository : BaseRepository, IUserRoleRepository
    {

        public bool InsertUserRoles(int userId, List<Role> roles, User user)
        {
            string sql = @"INSERT INTO [dbo].[Mapping_User_Role]
                           ([UserId]
                           ,[RoleId]
                           ,[CreatedBy]
                           ,[CreatedDate]
                           ,[UpdatedBy]
                           ,[UpdatedDate])
                     VALUES
                           (@UserId
                           ,@RoleId
                           ,@CreatedBy
                           ,@CreatedDate
                           ,@UpdatedBy
                           ,@UpdatedDate)";
            bool isSuccess = false;

            foreach (Role role in roles)
            {
                try
                {
                    int affectedRows = _connection.Execute(sql,
                        new
                        {
                            @UserId = userId,
                            @RoleId = role.Id,
                            @CreatedBy = role.CreatedBy,
                            @CreatedDate = DateTime.Now,
                            @UpdatedBy = role.UpdatedBy,
                            @UpdatedDate = DateTime.Now
                        },
                        _transaction);
                    if (affectedRows > 0)
                    {
                        isSuccess = true;
                    }
                }

                catch (Exception)
                {
                    isSuccess = false;
                }
            }

            return isSuccess;
        }
        public bool DeleteUserRoles(int userId, List<Role> roles)
        {
            string sql = @"DELETE FROM [dbo].[Mapping_User_Role]
                            WHERE UserId = @UserId
                            AND RoleId = @RoleId";
            bool isSuccess = false;

            foreach (Role role in roles)
            {
                try
                {
                    int affectedRows = _connection.Execute(sql,
                        new
                        {
                            @UserId = userId,
                            @RoleId = role.Id
                        },
                        _transaction);
                    if (affectedRows > 0)
                    {
                        isSuccess = true;
                    }
                }

                catch (Exception)
                {
                    isSuccess = false;
                }

            }

            return isSuccess;
        }

        public bool DeleteUserRoleByUserId(int userId)
        {
            string sql = @"DELETE FROM [dbo].[Mapping_User_Role]
                            WHERE UserId = @UserId";
            bool isSuccess = false;

            int affectedRows = _connection.Execute(sql,
                new
                {
                    @UserId = userId
                },
                _transaction);
            if (affectedRows > 0)
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        
    }
}