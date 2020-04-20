using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ChessLearning.Models;


namespace ChessLearning.Biz.ViewModels.AppointmentVMs
{
    public partial class AppointmentTemplateVM : BaseTemplateVM
    {
        public ExcelPropety StudentId_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.StudentId);
        [Display(Name = "学生昵称")]
        public ExcelPropety StudentNickName_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.StudentNickName);
        [Display(Name = "学生链接")]
        public ExcelPropety StudentUrl_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.StudentUrl);
        public ExcelPropety TeacherId_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.TeacherId);
        [Display(Name = "老师昵称")]
        public ExcelPropety TeacherNickName_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.TeacherNickName);
        [Display(Name = "老师链接")]
        public ExcelPropety TeacherUrl_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.TeacherUrl);
        [Display(Name = "预约状态")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.Status);
        [Display(Name = "预约发生时间")]
        public ExcelPropety DoingTime_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.DoingTime);
        [Display(Name = "预约标识")]
        public ExcelPropety RoomCode_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.RoomCode);
        [Display(Name = "学员选色")]
        public ExcelPropety StudentColor_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.StudentColor);
        [Display(Name = "预约创建时间")]
        public ExcelPropety CreatedTime_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.CreatedTime);
        [Display(Name = "预约创建人")]
        public ExcelPropety CreatedUserId_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.CreatedUserId);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<Appointment>(x => x.Remark);

	    protected override void InitVM()
        {
        }

    }

    public class AppointmentImportVM : BaseImportVM<AppointmentTemplateVM, Appointment>
    {

    }

}
