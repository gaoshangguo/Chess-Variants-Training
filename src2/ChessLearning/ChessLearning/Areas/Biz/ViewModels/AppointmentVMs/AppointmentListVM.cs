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
            var items = new List<GridAction>();
            items.Add(this.MakeAction("Appointment", "Create", "新建预约", "新建预约", GridActionParameterTypesEnum.NoId, "Biz", 400).SetShowInRow(false));
            items.Add(this.MakeAction("Appointment", "Edit", "确认预约", "确认预约", GridActionParameterTypesEnum.SingleId, "Biz", 400).SetShowInRow(true).SetBindVisiableColName("Confrim").SetHideOnToolBar(true));
            items.Add(this.MakeAction("Appointment", "Play", "开始下棋", "开始下棋", GridActionParameterTypesEnum.SingleId, "Biz",whereStr: x => x.TeacherUrl).SetShowInRow(true).SetShowDialog(true).SetIsRedirect(true).SetBindVisiableColName("Play").SetHideOnToolBar(true));
            items.Add(this.MakeStandardAction("Appointment", GridActionStandardTypesEnum.Details, "详细", "Biz", dialogWidth: 800));
            return items;
        }

        protected override IEnumerable<IGridColumn<Appointment_View>> InitGridHeader()
        {
            var role = 1;
            if (LoginUserInfo.Roles.Any(y => y.RoleCode == "002"))
            {
                role = 2;
            }
            else if (LoginUserInfo.Roles.Any(y => y.RoleCode == "003"))
            {
                role = 3;
            }
            return new List<GridColumn<Appointment_View>>{
                this.MakeGridHeader(x => x.StudentNickName),
                this.MakeGridHeader(x => x.TeacherNickName),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.DoingTime),
                this.MakeGridHeader(x => x.StudentColor),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeader(x=>"Confrim").SetHide().SetFormat((e,v)=>{
                    if (role == 2)
                    {
                        if (e.StudentId == Guid.Empty)
                        {
                            return "true";
                        }
                        else
                        {
                            return "false";
                        }
                    }
                    else if (role == 3)
                    {
                        if (e.TeacherId == Guid.Empty)
                        {
                            return "true";
                        }
                        else
                        {
                            return "false";
                        }
                    }
                    else {
                        return "false";
                    }
                }),
                 this.MakeGridHeader(x=>"Play").SetHide().SetFormat((e,v)=>{
                     if (e.StudentId != Guid.Empty&&e.TeacherId!=Guid.Empty)
                     {
                         return "true";
                     }
                     else
                     {
                         return "false";
                     }
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Appointment_View> GetSearchQuery()
        {
            var query = DC.Set<Appointment>()
                .CheckEqual(Searcher.Status, x => x.Status)
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
                    CreateTime = x.CreateTime,
                    CreateBy = x.CreateBy,
                    Remark = x.Remark,
                });

            var role = 1;
            if (LoginUserInfo.Roles.Any(y => y.RoleCode == "002"))
            {
                role = 2;
            }
            else if (LoginUserInfo.Roles.Any(y => y.RoleCode == "003"))
            {
                role = 3;
            }
            if (role == 2)
            {
                query = query.Where(p => p.StudentId == LoginUserInfo.Id || p.StudentId == Guid.Empty);
            }
            else if (role == 3)
            {
                query = query.Where(p => p.TeacherId == LoginUserInfo.Id || p.TeacherId == Guid.Empty);
            }
            return query.OrderByDescending(p => p.ID);
        }
    }

    public class Appointment_View : Appointment
    {

    }
}
