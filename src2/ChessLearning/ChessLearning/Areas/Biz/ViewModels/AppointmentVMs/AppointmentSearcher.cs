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
    public partial class AppointmentSearcher : BaseSearcher
    {
        //[Display(Name = "学生昵称")]
        //public String StudentNickName { get; set; }
        //[Display(Name = "老师昵称")]
        //public String TeacherNickName { get; set; }
        [Display(Name = "预约状态")]
        public AppointmentStatusEnum? Status { get; set; }
        //[Display(Name = "预约创建人")]
        //public String CreatedUserId { get; set; }

        protected override void InitVM()
        {
        }

    }
}
