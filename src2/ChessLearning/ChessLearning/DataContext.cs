using ChessLearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;

namespace ChessLearning
{
    public class DataContext : FrameworkContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<CustomUser> CustomUsers { get; set; }
        public DataContext(CS cs) : base(cs)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype, string version = null) : base(cs, dbtype, version)
        {
        }

    }

    /// <summary>
    /// 为EF的Migration准备的辅助类，填写完整连接字符串和数据库类型
    /// 就可以使用Add-Migration和Update-Database了
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext("server=47.97.163.250;port=3306;database=chessdb;user=root;password=Eric@2019;CharSet=utf8;", DBTypeEnum.MySql);
        }
    }

}
