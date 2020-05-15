﻿using Microsoft.EntityFrameworkCore;
using RestApi.NetCore.Data;
using RestApi.NetCore.Interfaces;
using RestApi.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.NetCore.Repositories
{
    public class FeversIntervalRepository :IFeverIntervalInterface
    {
        private readonly ApplicationDbContext _context;
        public FeversIntervalRepository(ApplicationDbContext context)
        {
            _context = context;
        }





        public void FeverIntervalMethod(BodyTemperature bodyTemperature)
        {
            FeverInterval fever = GetUserLastFeverInterval(bodyTemperature.UserId);


            if (bodyTemperature.Temperature > 37.5)
            {
                if (fever == null)
                {
                    FeverInterval feverInterval = new FeverInterval();
                    feverInterval.UserId = bodyTemperature.UserId;
                    feverInterval.StartedTemperature = bodyTemperature.Temperature;
                    feverInterval.StartDate = DateTime.Now.Date;
                    //feverInterval.EndDate = null;

                    PostFeverInterval1(feverInterval);
                }
                else if (fever.EndDate != null)
                {
                    FeverInterval feverInterval = new FeverInterval();
                    feverInterval.UserId = bodyTemperature.UserId;
                    feverInterval.StartedTemperature = bodyTemperature.Temperature;
                    feverInterval.StartDate = DateTime.Now.Date;
                    //feverInterval.EndDate = null;

                    PostFeverInterval1(feverInterval);
                }
            }
            else
            {
                if (fever != null && fever.EndDate == null)
                {
                    FeverInterval feverInterval = new FeverInterval();
                    feverInterval.Id = fever.Id;
                    feverInterval.UserId = fever.UserId;
                    feverInterval.StartedTemperature = fever.StartedTemperature;
                    feverInterval.StartDate = fever.StartDate;
                    feverInterval.EndDate = DateTime.Now.Date;
                    PutFeverInterval(fever.Id, feverInterval);
                }

            }

        }

        public FeverInterval GetUserLastFeverInterval(string userId)
        {
            var feverInterval = _context.FeverIntervals.AsNoTracking().AsEnumerable().LastOrDefault(i => i.UserId == userId);

            return feverInterval;
        }


        public void PostFeverInterval1(FeverInterval feverInterval)
        {
            _context.FeverIntervals.Add(feverInterval);
            //await _context.SaveChangesAsync();
        }


        public void PutFeverInterval(int id, FeverInterval feverInterval)
        {
            _context.FeverIntervals.Attach(feverInterval);
            _context.Entry(feverInterval).State = EntityState.Modified;
            // _context.SaveChangesAsync();            

        }

    }
}
