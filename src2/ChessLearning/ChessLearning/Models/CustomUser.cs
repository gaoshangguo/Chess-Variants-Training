using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace ChessLearning.Models
{
    public class CustomUser : FrameworkUserBase
    {
        [Display(Name = "头衔")]
        public string Title { get; set; }
        [Display(Name = "分数")]
        public int Score { get; set; }
    }
}
