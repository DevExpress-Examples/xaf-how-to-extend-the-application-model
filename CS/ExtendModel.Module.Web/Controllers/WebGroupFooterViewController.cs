﻿using System;
using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;

namespace ExtendModel.Module.Web.Controllers {
    public class WebGroupFooterViewController : ViewController<ListView> {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            IModelListViewExtender modelListView = View.Model as IModelListViewExtender;
            if (modelListView != null && modelListView.IsGroupFooterVisible) {
                ASPxGridListEditor gridListEditor = View.Editor as ASPxGridListEditor;
                if (gridListEditor != null) {
                    ASPxGridView gridView = gridListEditor.Grid;
                    gridView.Settings.ShowGroupFooter = GridViewGroupFooterMode.VisibleAlways;
                    foreach (IModelColumn modelColumn in View.Model.Columns) {
                        IModelColumnExtender modelColumnExtender = modelColumn as IModelColumnExtender;
                        if (modelColumnExtender != null &&
                            modelColumnExtender.GroupFooterSummaryType != SummaryItemType.None) {
                            string fieldName = modelColumn.ModelMember.MemberInfo.BindingName.ToUpper();
                            ASPxSummaryItem summaryItem = null;
                            foreach (ASPxSummaryItem currentItem in gridView.GroupSummary) {
                                if (currentItem.FieldName == fieldName) {
                                    currentItem.ShowInGroupFooterColumn = modelColumn.Caption.ToUpper();
                                    summaryItem = currentItem;
                                    break;
                                }
                            }
                            if (summaryItem == null) {
                                summaryItem = new ASPxSummaryItem(
                                    fieldName, modelColumnExtender.GroupFooterSummaryType);
                                summaryItem.ShowInGroupFooterColumn = modelColumn.Caption.ToUpper();
                                gridView.GroupSummary.Add(summaryItem);
                            }
                        }
                    }
                }
            }
        }
    }
}
