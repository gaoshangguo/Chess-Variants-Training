using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace ChessLearning.Models
{
    public enum AppointmentStatusEnum
    {
        [Display(Name = "发起预约")]
        Init,
        [Display(Name = "确认预约")]
        Confrim,

        [Display(Name = "预约进行中")]
        Doing,
        [Display(Name = "预约完成")]
        Done,
    }

    public enum StudentColorEnum
    {
        [Display(Name = "白色")]
        White,
        [Display(Name = "黑色")]
        Black
    }
    public class Appointment : BasePoco
    {

        public string StudentId { get; set; }
        
        [Display(Name = "学生昵称")]
        public string StudentNickName { get; set; }

        [Display(Name = "学生链接")]
        public string StudentUrl { get; set; }
        
        public string TeacherId { get; set; }
        
        [Display(Name = "老师昵称")]
        public string TeacherNickName { get; set; }
        [Display(Name = "老师链接")]
        public string TeacherUrl { get; set; }
        [Display(Name = "预约状态")]
        public AppointmentStatusEnum Status { get; set; }

        [Display(Name = "预约发生时间")]
        public string DoingTime { get; set; }
        
        [Display(Name = "预约标识")]
        public string RoomCode { get; set; }
        
        [Display(Name = "学员选色")]
        public StudentColorEnum StudentColor { get; set; }

        [Display(Name = "预约创建时间")]
        public string CreatedTime { get; set; }
        
        [Display(Name = "预约创建人")]
        public string CreatedUserId { get; set; }
        
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
