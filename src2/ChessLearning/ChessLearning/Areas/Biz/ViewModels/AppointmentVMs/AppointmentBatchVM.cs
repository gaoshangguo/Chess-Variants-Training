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
    public partial class AppointmentBatchVM : BaseBatchVM<Appointment, Appointment_BatchEdit>
    {
        public AppointmentBatchVM()
        {
            ListVM = new AppointmentListVM();
            LinkedVM = new Appointment_BatchEdit();
        }

    }

	/// <summary>
    /// 批量编辑字段类
    /// </summary>
    public class Appointment_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
