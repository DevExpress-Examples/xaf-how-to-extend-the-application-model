using System;
using DevExpress.Blazor;
using DevExpress.Blazor.Internal;
using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Grid;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;

namespace ExtendModel.Module.Win.Controllers {
    public class BlazorGroupFooterViewController : ViewController<ListView> {

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            IModelListViewExtender modelListView = View.Model as IModelListViewExtender;
            if (modelListView != null && modelListView.IsGroupFooterVisible) {
                DxGridListEditor gridListEditor = View.Editor as DxGridListEditor;
                if (gridListEditor != null) {
                    var dataGridAdapter = gridListEditor.GetGridAdapter();
                    dataGridAdapter.GridModel.GroupFooterDisplayMode = DevExpress.Blazor.GridGroupFooterDisplayMode.Always;
                    foreach (IModelColumn modelColumn in View.Model.Columns) {
                        IModelColumnExtender modelColumnExtender = modelColumn as IModelColumnExtender;
                        if (modelColumnExtender != null &&
                            modelColumnExtender.GroupFooterSummaryType != DevExpress.Data.SummaryItemType.None) {
                            IDxGridSummaryItemsOwner summaryItemsOwner = (IDxGridSummaryItemsOwner)dataGridAdapter;
                            var summaryItem = (DxGridSummaryItemWrapper)summaryItemsOwner.CreateItem(modelColumn.Id, modelColumnExtender.GroupFooterSummaryType);
                            summaryItem.SummaryItemModel.FooterColumnName = modelColumn.Id;
                            summaryItemsOwner.GroupSummary.Add(summaryItem);
                        }
                    }
                  
                }
            }
        }
    }
}
