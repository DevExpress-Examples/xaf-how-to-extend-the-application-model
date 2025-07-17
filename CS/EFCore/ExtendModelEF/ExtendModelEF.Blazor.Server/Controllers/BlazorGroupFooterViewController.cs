using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Model;

namespace ExtendModel.Module.Win.Controllers;
public class BlazorGroupFooterViewController : ViewController<ListView> {
    protected override void OnViewControlsCreated() {
        base.OnViewControlsCreated();
        if (View.Model is IModelListViewExtender modelListView && modelListView.IsGroupFooterVisible && View.Editor is DxGridListEditor gridListEditor) {
            gridListEditor.GridModel.GroupFooterDisplayMode = DevExpress.Blazor.GridGroupFooterDisplayMode.Always;
            foreach (var modelColumn in View.Model.Columns) {
                if (modelColumn is IModelColumnExtender modelColumnExtender && modelColumnExtender.GroupFooterSummaryType != SummaryItemType.None) {
                    var summaryItem = (DxGridSummaryItemWrapper)gridListEditor.GridSummary.CreateItem(modelColumn.Id, modelColumnExtender.GroupFooterSummaryType);
                    summaryItem.SummaryItemModel.FooterColumnName = modelColumn.Id;
                    gridListEditor.GridSummary.GroupSummary.Add(summaryItem);
                }
            }
        }
    }
}

