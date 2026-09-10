using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using ExtendModel.Module;

namespace ExtendModel.Blazor.Server.Controllers {
    public class BlazorGroupFooterViewController : ViewController<ListView> {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if (View.Model is IModelListViewExtender modelListView && modelListView.IsGroupFooterVisible && View.Editor is DxGridListEditor gridListEditor) {
                gridListEditor.GridModel.GroupFooterDisplayMode = DevExpress.Blazor.GridGroupFooterDisplayMode.Always;
                foreach (var column in gridListEditor.Columns) {
                    if (column.ModelColumn is IModelColumnExtender modelColumnExtender && modelColumnExtender.GroupFooterSummaryType != SummaryItemType.None) {
                        // Try to find an existing summary item with the specified summary type:
                        var summaryItem = (DxGridSummaryItemWrapper)gridListEditor.GridSummary.GroupSummary.FirstOrDefault(
                            item => item.FieldName == column.FieldName && item.SummaryType == modelColumnExtender.GroupFooterSummaryType);
                        if (summaryItem is null) {
                            // No existing summary item - create a new one:
                            summaryItem = (DxGridSummaryItemWrapper)gridListEditor.GridSummary.CreateItem(column.FieldName, modelColumnExtender.GroupFooterSummaryType);
                            gridListEditor.GridSummary.GroupSummary.Add(summaryItem);
                        }
                        // Ensure the summary item is displayed in the group footer:
                        summaryItem.SummaryItemModel.FooterColumnName = column.FieldName;
                    }
                }
            }
        }
    }
}
