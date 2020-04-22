using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using ChessLearning.Biz.ViewModels.AppointmentVMs;
using System.Linq;
using ChessLearning.Common;
using Newtonsoft.Json;
using ChessLearning.Models;

namespace ChessLearning.Controllers
{
    [Area("Biz")]
    [ActionDescription("预约管理")]
    public partial class AppointmentController : BaseController
    {
        #region 搜索
        [ActionDescription("搜索")]
        public ActionResult Index()
        {
            var vm = CreateVM<AppointmentListVM>();
            return PartialView(vm);
        }

        [ActionDescription("搜索")]
        [HttpPost]
        public string Search(AppointmentListVM vm)
        {
            return vm.GetJson(false);
        }

        #endregion

        #region 新建
        [ActionDescription("新建")]
        public ActionResult Create()
        {
            var vm = CreateVM<AppointmentVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("新建")]
        public ActionResult Create(AppointmentVM vm)
        {
            vm.Entity.CreateTime = DateTime.Now;
            vm.Entity.CreateBy = LoginUserInfo.Name;
            var role = 2;
            if (LoginUserInfo.Roles.Any(y => y.RoleCode == "003"))
            {
                role = 3;
            }
            if (role == 2)
            {
                vm.Entity.StudentId = LoginUserInfo.Id;
                vm.Entity.StudentNickName = LoginUserInfo.Name;
            }
            else
            {
                vm.Entity.TeacherId = LoginUserInfo.Id;
                vm.Entity.TeacherNickName = LoginUserInfo.Name;
            }
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    vm.DoReInit();
                    return PartialView(vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGrid();
                }
            }
        }
        #endregion

        #region 修改
        [ActionDescription("修改")]
        public ActionResult Edit(string id)
        {
            var vm = CreateVM<AppointmentVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("修改")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult Edit(AppointmentVM vm)
        {
            var role = 2;
            if (LoginUserInfo.Roles.Any(y => y.RoleCode == "003"))
            {
                role = 3;
            }
            if (role == 2)
            {
                vm.Entity.StudentId = LoginUserInfo.Id;
                vm.Entity.StudentNickName = LoginUserInfo.Name;
            }
            else
            {
                vm.Entity.TeacherId = LoginUserInfo.Id;
                vm.Entity.TeacherNickName = LoginUserInfo.Name;
            }
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                vm.DoEdit();
                if (!ModelState.IsValid)
                {
                    vm.DoReInit();
                    return PartialView(vm);
                }
                else
                {
                    string writeUser = "";
                    string blackUser = "";
                    if (vm.Entity.StudentColor == Models.StudentColorEnum.White)
                    {
                        writeUser = vm.Entity.StudentNickName;
                        blackUser = vm.Entity.TeacherNickName;
                    }
                    else
                    {
                        blackUser = vm.Entity.StudentNickName;
                        writeUser = vm.Entity.TeacherNickName;
                    }
                    var resultStr = WebHelper.HttpGetRequest("http://47.97.163.250:3000/create?whiteName=" + writeUser + "&blackName=" + blackUser);
                    var resultObj = JsonConvert.DeserializeObject<ChessAddress>(resultStr);
                    if (resultObj != null)
                    {
                        if (vm.Entity.StudentColor == Models.StudentColorEnum.White)
                        {
                            vm.Entity.StudentUrl = resultObj.white;
                            vm.Entity.TeacherUrl = resultObj.black;
                        }
                        else
                        {
                            vm.Entity.StudentUrl = resultObj.black;
                            vm.Entity.TeacherUrl = resultObj.white;
                        }
                        vm.Entity.Status = AppointmentStatusEnum.Confrim;
                        vm.DoEdit();
                    }

                    return FFResult().CloseDialog().RefreshGridRow(vm.Entity.ID);
                }
            }
        }
        #endregion

        #region 删除
        [ActionDescription("删除")]
        public ActionResult Delete(string id)
        {
            var vm = CreateVM<AppointmentVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("删除")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = CreateVM<AppointmentVM>(id);
            vm.DoDelete();
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid();
            }
        }
        #endregion

        #region 详细
        [ActionDescription("详细")]
        public ActionResult Details(string id)
        {
            var vm = CreateVM<AppointmentVM>(id);
            return PartialView(vm);
        }
        #endregion

        #region 批量修改
        [HttpPost]
        [ActionDescription("批量修改")]
        public ActionResult BatchEdit(string[] IDs)
        {
            var vm = CreateVM<AppointmentBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("批量修改")]
        public ActionResult DoBatchEdit(AppointmentBatchVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid || !vm.DoBatchEdit())
            {
                return PartialView("BatchEdit", vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert("操作成功，共有" + vm.Ids.Length + "条数据被修改");
            }
        }
        #endregion

        #region 批量删除
        [HttpPost]
        [ActionDescription("批量删除")]
        public ActionResult BatchDelete(string[] IDs)
        {
            var vm = CreateVM<AppointmentBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("批量删除")]
        public ActionResult DoBatchDelete(AppointmentBatchVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return PartialView("BatchDelete", vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert("操作成功，共有" + vm.Ids.Length + "条数据被删除");
            }
        }
        #endregion

        #region 导入
        [ActionDescription("导入")]
        public ActionResult Import()
        {
            var vm = CreateVM<AppointmentImportVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("导入")]
        public ActionResult Import(AppointmentImportVM vm, IFormCollection nouse)
        {
            if (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData())
            {
                return PartialView(vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert("成功导入 " + vm.EntityList.Count.ToString() + " 行数据");
            }
        }
        #endregion

        #region 开始下棋
        [ActionDescription("开始下棋")]
        public ActionResult Play(string id)
        {
            var vm = CreateVM<AppointmentVM>(id);
            var role = 2;
            if (LoginUserInfo.Roles.Any(y => y.RoleCode == "003"))
            {
                role = 3;
            }
            if (role == 2)
            {
                ViewBag.Url = "http://47.97.163.250:3000" + vm.Entity.StudentUrl;
            }
            else if (role == 3)
            {
                ViewBag.Url = "http://47.97.163.250:3000" + vm.Entity.TeacherUrl;
            }
            return PartialView(vm);
        }
        #endregion

        [ActionDescription("导出")]
        [HttpPost]
        public IActionResult ExportExcel(AppointmentListVM vm)
        {
            vm.SearcherMode = vm.Ids != null && vm.Ids.Count > 0 ? ListVMSearchModeEnum.CheckExport : ListVMSearchModeEnum.Export;
            var data = vm.GenerateExcel();
            return File(data, "application/vnd.ms-excel", $"Export_Appointment_{DateTime.Now.ToString("yyyy-MM-dd")}.xls");
        }

    }
}
