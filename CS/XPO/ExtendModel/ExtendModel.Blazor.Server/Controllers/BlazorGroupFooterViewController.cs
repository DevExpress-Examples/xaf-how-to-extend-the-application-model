using System;
using DevExpress.Blazor;
using DevExpress.Blazor.Internal;
using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Editors;
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
                    var gridAdapter = gridListEditor.GetGridAdapter();
                    var gridModel = gridAdapter.GridModel;
                    gridModel.ShowGroupPanel = true;
                    gridModel.ShowGroupedColumns = true;
                    var oldSummary = gridModel.GroupSummary;



                    foreach (IModelColumn modelColumn in View.Model.Columns) {
                        IModelColumnExtender modelColumnExtender = modelColumn as IModelColumnExtender;
                        if (modelColumnExtender != null && modelColumnExtender.GroupFooterSummaryType != DevExpress.Data.SummaryItemType.None) {
                            gridModel.GroupSummary = CreateSummary(oldSummary, modelColumnExtender.GroupFooterSummaryType, modelColumn.PropertyName);
                        }
                    }
                }
            }
        }
        private RenderFragment CreateSummary(RenderFragment oldSummary, DevExpress.Data.SummaryItemType summaryItemType, string propertyName) {
            return builder => {
                oldSummary(builder);
                builder.OpenComponent<DxGridSummaryItem>(0);
                switch (summaryItemType) {
                    case DevExpress.Data.SummaryItemType.Sum:
                        builder.AddAttribute(1, nameof(DxGridSummaryItem.SummaryType), GridSummaryItemType.Sum);
                        break;
                    case DevExpress.Data.SummaryItemType.Max:
                        builder.AddAttribute(1, nameof(DxGridSummaryItem.SummaryType), GridSummaryItemType.Max);
                        break;
                    case DevExpress.Data.SummaryItemType.Min:
                        builder.AddAttribute(1, nameof(DxGridSummaryItem.SummaryType), GridSummaryItemType.Min);
                        break;
                    case DevExpress.Data.SummaryItemType.Count:
                        builder.AddAttribute(1, nameof(DxGridSummaryItem.SummaryType), GridSummaryItemType.Count);
                        break;
                }
                builder.AddAttribute(2, nameof(DxGridSummaryItem.FieldName), propertyName);
                builder.AddAttribute(3, nameof(DxGridSummaryItem.Visible), true);
                builder.CloseComponent();
            };
        }


    }
}
