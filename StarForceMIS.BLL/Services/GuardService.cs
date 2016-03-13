﻿using StarForceMIS.BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarForceMIS.Web.Models;
using StarForceMIS.BLL.Model;

namespace StarForceMIS.BLL.Services
{
    public class GuardService : IGuardService
    {
        public Callback<GuardViewModel> AddNewGuard(GuardViewModel model)
        {
            using (var db = new StarforceDBEntities())
            {
                var result = new Callback<GuardViewModel>();
                try
                {
                    var entity = new Guard(model);
                    db.Guards.Add(entity);
                    db.SaveChanges();
                    model.ID = entity.ID;
                    result.Data = model;
                    result.SuccessResult(string.Empty);
                    return result;
                }
                catch (Exception e)
                {
                    result.FailResult(e.Message);
                    return result;
                }
            }
        }

        public List<GuardViewModel> RetrieveGuards()
        {
            using (var db = new StarforceDBEntities())
            {
                var entities = db.Guards.Select(entity => new GuardViewModel { 
                            ID = entity.ID
                            ,FirstName = entity.FirstName
                            ,MiddleName = entity.MiddleName
                            ,LastName = entity.LastName
                            ,CallSign = entity.CallSign
                            ,LicensedNumber = entity.LicesnedNumber
                            ,ExpiredDate = entity.DateExpiry
                            ,Designation = entity.Position
                            ,IsReliever = entity.IsReliver
                }).ToList();
                return entities;
            }
        }

        public List<GuardViewModel> SearchGuard(string queryString)
        {
            using (var db = new StarforceDBEntities()) 
            {
                var entities = db.Guards
                        .Where(i => i.FirstName.Contains(queryString) || i.LastName.Contains(queryString))
                        .Select(entity => new GuardViewModel { 
                            ID = entity.ID
                            ,FirstName = entity.FirstName
                            ,MiddleName = entity.MiddleName
                            ,LastName = entity.LastName
                            ,CallSign = entity.CallSign
                            ,LicensedNumber = entity.LicesnedNumber
                            ,ExpiredDate = entity.DateExpiry
                            ,Designation = entity.Position
                            ,IsReliever = entity.IsReliver
                }).ToList();
                return entities;
            }
        }

        public Callback<GuardViewModel> UpdateGuard(GuardViewModel model)
        {
            using (var db = new StarforceDBEntities())
            {
                var result = new Callback<GuardViewModel>();
                try
                {
                    var entity = db.Guards.Where(i => i.ID.Equals(model.ID)).FirstOrDefault();
                    entity.FirstName = model.FirstName;
                    entity.MiddleName = model.MiddleName;
                    entity.LastName = model.LastName;
                    entity.CallSign = model.CallSign;
                    entity.LicesnedNumber = model.LicensedNumber;
                    entity.DateExpiry = model.ExpiredDate;
                    entity.Position = model.Designation;
                    entity.IsReliver = model.IsReliever;
                    entity.UpdatedBy = 1;
                    entity.UpdatedDate = DateTime.UtcNow;
                    db.SaveChanges();
                    result.Data = model;
                    result.SuccessResult(string.Empty);
                }
                catch (Exception e)
                {
                    result.FailResult(e.Message);
                }

                return result;

            }
        }

        public Callback<GuardViewModel> RemoveGuard(int id)
        {
            throw new NotImplementedException();
        }


        public GuardViewModel GetGuardByID(long id)
        {
            using (var db = new StarforceDBEntities())
            {
                GuardViewModel model = new GuardViewModel();
                var entity = db.Guards.Where(i => i.ID.Equals(id)).FirstOrDefault();
                if (entity != null)
                    model = new GuardViewModel(entity);

                return model;

            }
        }
    }
}
