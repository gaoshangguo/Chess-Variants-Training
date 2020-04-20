using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ChessLearning.Models;


namespace ChessLearning.Biz.ViewModels.AppointmentVMs
{
    public partial class AppointmentListVM : BasePagedListVM<Appointment_View, AppointmentSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Appointment", GridActionStandardTypesEnum.Create, "新建","Biz", dialogWidth: 800),
                this.MakeStandardAction("Appointment", GridActionStandardTypesEnum.Edit, "修改","Biz", dialogWidth: 800),
                this.MakeStandardAction("Appointment", GridActionStandardTypesEnum.Delete, "删除", "Biz",dialogWidth: 800),
                this.MakeStandardAction("Appointment", GridActionStandardTypesEnum.Details, "详细","Biz", dialogWidth: 800),
                this.MakeStandardAction("Appointment", GridActionStandardTypesEnum.BatchEdit, "批量修改","Biz", dialogWidth: 800),
                this.MakeStandardAction("Appointment", GridActionStandardTypesEnum.BatchDelete, "批量删除","Biz", dialogWidth: 800),
                this.MakeStandardAction("Appointment", GridActionStandardTypesEnum.Import, "导入","Biz", dialogWidth: 800),
                this.MakeStandardAction("Appointment", GridActionStandardTypesEnum.ExportExcel, "导出","Biz"),
            };
        }

        protected override IEnumerable<IGridColumn<Appointment_View>> InitGridHeader()
        {
            return new List<GridColumn<Appointment_View>>{
                this.MakeGridHeader(x => x.StudentId),
                this.MakeGridHeader(x => x.StudentNickName),
                this.MakeGridHeader(x => x.StudentUrl),
                this.MakeGridHeader(x => x.TeacherId),
                this.MakeGridHeader(x => x.TeacherNickName),
                this.MakeGridHeader(x => x.TeacherUrl),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.DoingTime),
                this.MakeGridHeader(x => x.RoomCode),
                this.MakeGridHeader(x => x.StudentColor),
                this.MakeGridHeader(x => x.CreatedTime),
                this.MakeGridHeader(x => x.CreatedUserId),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Appointment_View> GetSearchQuery()
        {
            var query = DC.Set<Appointment>()
                .CheckContain(Searcher.StudentNickName, x=>x.StudentNickName)
                .CheckContain(Searcher.TeacherNickName, x=>x.TeacherNickName)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckContain(Searcher.CreatedUserId, x=>x.CreatedUserId)
                .Select(x => new Appointment_View
                {
				    ID = x.ID,
                    StudentId = x.StudentId,
                    StudentNickName = x.StudentNickName,
                    StudentUrl = x.StudentUrl,
                    TeacherId = x.TeacherId,
                    TeacherNickName = x.TeacherNickName,
                    TeacherUrl = x.TeacherUrl,
                    Status = x.Status,
                    DoingTime = x.DoingTime,
                    RoomCode = x.RoomCode,
                    StudentColor = x.StudentColor,
                    CreatedTime = x.CreatedTime,
                    CreatedUserId = x.CreatedUserId,
                    Remark = x.Remark,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Appointment_View : Appointment{

    }
}
