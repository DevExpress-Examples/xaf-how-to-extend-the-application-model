using System;
using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ExtendModel.Module.Win.Controllers {
    public class WinGroupFooterViewController : ViewController<ListView> {
        private void View_ModelSaved(object sender, EventArgs e) {
            if (View.Model is IModelListViewExtender modelListView && modelListView.IsGroupFooterVisible) {
                if (View.Editor is GridListEditor gridListEditor) {
                    GridView gridView = gridListEditor.GridView;
                    for (int i = 0; i < gridView.GroupSummary.Count; i++) {
                        if (View.Model.Columns[
                            gridView.GroupSummary[i].FieldName] is IModelColumnExtender modelColumn) {
                            modelColumn.GroupFooterSummaryType = gridView.GroupSummary[i].SummaryType;
                        }
                    }
                }
            }
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if (View.Model is IModelListViewExtender modelListView && modelListView.IsGroupFooterVisible) {
                if (View.Editor is GridListEditor gridListEditor) {
                    GridView gridView = gridListEditor.GridView;
                    gridView.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
                    foreach (var column in gridListEditor.Columns) {
                        if (column.ModelColumn is IModelColumnExtender modelColumnExtender && modelColumnExtender.GroupFooterSummaryType != SummaryItemType.None) {
                            gridView.GroupSummary.Add(modelColumnExtender.GroupFooterSummaryType, column.Id, column.Column);
                        }
                    }
                }
            }
        }
        protected override void OnActivated() {
            base.OnActivated();
            View.ModelSaved += View_ModelSaved;
        }
        protected override void OnDeactivated() {
            View.ModelSaved -= View_ModelSaved;
            base.OnDeactivated();
        }
    }
}
