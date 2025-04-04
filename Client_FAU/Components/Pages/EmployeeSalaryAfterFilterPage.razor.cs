﻿using Client_FAU.Business.Interfaces;
using Client_FAU.Components.Ingredients;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using Models;
using System.Threading.Tasks;

namespace Client_FAU.Components.Pages
{
    public partial class EmployeeSalaryAfterFilterPage
    {
        [Inject]
        private EmployeeSalary_Int? ESBsn {  get; set; }
        [Inject]
        private NavigationManager? Navigation { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [SupplyParameterFromForm]
        private EmployeeSalary? Model { get; set; } = new();


        private IEnumerable<EmployeeSalary>? data;
        private bool isLoading = false;

        async Task ShowLoading()
        {
            isLoading = true;

            await Task.Yield();

            isLoading = false;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            int month = 0, year = 0;
            var uri = new Uri(Navigation!.Uri);
            var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
            if (query.TryGetValue("month", out var value))
            {
                month = !string.IsNullOrEmpty(value) ? int.Parse(value!) : 0;
            }
            if (query.TryGetValue("year", out var value2))
            {
                year = !string.IsNullOrEmpty(value2) ? int.Parse(value2!) : 0;
            }

            if (month == 0 || year == 0) { return; }
            await FindEmployeeSalariesDatabase(month, year);
            StateHasChanged();
        }

        private void ClearForm() => Model = new();

        private async Task FindEmployeeSalariesDatabase(int month, int year)
        {
            try
            {
                var result = await ESBsn!.GetTheEmployeeSalariesByMonth(month, year);

                if(result != null)
                {
                    Lists.employeeSalaries = result;
                } else
                {
                    Lists.employeeSalaries = new List<EmployeeSalary>();
                }

                data = Lists.employeeSalaries;
            } catch (Exception ex)
            {
                Notification.message = ex.Message;
            }

            //Thread.Sleep(100);
            //await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private void SetEditState(EmployeeSalary employeeSalary)
        {
            Model!.EmpSalCode = employeeSalary.EmpSalCode;
            Model.FullName = employeeSalary.FullName;
            Model.BranchName = employeeSalary.BranchName;
            Model.Month = employeeSalary.Month;
            Model.WorkDays = employeeSalary.WorkDays;
            Model.PriceTotals = employeeSalary.PriceTotals;
            Model.Note = employeeSalary.Note;
            Model.IsPaid = employeeSalary.IsPaid;
            Model.AccountCode = employeeSalary.AccountCode;
            Model.ProofImage = employeeSalary.ProofImage;
        }

        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var buffer = new byte[file.Size];
            await file.OpenReadStream().ReadExactlyAsync(buffer);
            Model!.ProofImage = buffer;
        }

        private void SetEmployeeSalaryProperties()
        {
            Model!.Note = !string.IsNullOrEmpty(Model.Note) ? Model.Note!.Trim() : string.Empty;
        }

        private async Task EditEmployeeSalaryDatabase()
        {
            try
            {
                SetEmployeeSalaryProperties();
                var result = await ESBsn!.EditAnExistEmployeeSalary(Model!);
                if (result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateEmployeeSalariesData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            }
            catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();

            Thread.Sleep(600);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private void UpdateEmployeeSalariesData(EmployeeSalary employeeSalary)
        {
            if (employeeSalary == null) { return; }
            var getES = Lists.employeeSalaries!.Where(x => x.EmpSalCode == employeeSalary.EmpSalCode).FirstOrDefault();
            if (getES == default || getES == null)
            {
                Lists.employeeSalaries!.Insert(0, employeeSalary);
            }
            else
            {
                var index = Lists.employeeSalaries!.IndexOf(getES);
                Lists.employeeSalaries[index] = employeeSalary;
            }
        }
    }
}
