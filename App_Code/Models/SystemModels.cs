﻿using CMS.CMSHelper;
using CMS.SiteProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

public class SystemModels {
    public static List<UserInfoObject> UserList() {
        return UserInfoProvider.GetFullUsers("(UserIsHidden = 0 or UserIsHidden is NULL) and UserEnabled = 1", "", 0, "UserID,FullName,Email,UserDateOfBirth,UserCreated,LastLogon").Tables[0].AsEnumerable().Select(s => new UserInfoObject {
            UserID = s.Field<int>("UserID"), Email = s.Field<string>("Email"), FullName = s.Field<string>("FullName"), UserDateOfBirth = s.Field<DateTime?>("UserDateOfBirth"), UserCreated = s.Field<DateTime?>("UserCreated"), LastLogon = s.Field<DateTime?>("LastLogon")
        }).ToList();
    }
    public static List<UserInfoObject> UserListByRole(string RoleName) {
        int RoleId = RoleInfoProvider.GetRoleInfo(RoleName, CMSContext.CurrentSite.SiteName).RoleID;
        return UserInfoProvider.GetFullUsers("(UserIsHidden = 0 or UserIsHidden is NULL) and UserEnabled = 1", "", 0, "UserID,FullName,Email,UserDateOfBirth,UserCreated,LastLogon").Tables[0].AsEnumerable()
            .Join(UserRoleInfoProvider.GetUserRoles("RoleID = " + RoleId, null, 0, null).Tables[0].AsEnumerable(), fu => fu.Field<int>("UserID"), ur => ur.Field<int>("UserID"), (fu , ur) => new {fu, ur})
            .Select(s => new UserInfoObject {
                UserID = s.fu.Field<int>("UserID"), Email = s.fu.Field<string>("Email"), FullName = s.fu.Field<string>("FullName"), UserDateOfBirth = s.fu.Field<DateTime?>("UserDateOfBirth"), UserCreated = s.fu.Field<DateTime?>("UserCreated"), LastLogon = s.fu.Field<DateTime?>("LastLogon")
            }).ToList();
    }
    public static List<UserInfoObject> UserListNotInRole(string RoleName) {
        int RoleId = RoleInfoProvider.GetRoleInfo(RoleName, CMSContext.CurrentSite.SiteName).RoleID;
        return UserInfoProvider.GetFullUsers("(UserIsHidden = 0 or UserIsHidden is NULL) and UserEnabled = 1", "", 0, "UserID,FullName,Email,UserDateOfBirth").Tables[0].AsEnumerable()
            .Where(w => !UserRoleInfoProvider.GetUserRoles("RoleID = " + RoleId, null, 0, null).Tables[0].AsEnumerable().Select(s => s.Field<int>("UserID")).Contains(w.Field<int>("UserID")))
            .Select(s => new UserInfoObject {
                UserID = s.Field<int>("UserID"), Email = s.Field<string>("Email"), FullName = s.Field<string>("FullName"), UserDateOfBirth = s.Field<DateTime?>("UserDateOfBirth")
            }).ToList();
    }
    public static void UserCreated(string Email, string FullName, DateTime UserDateOfBirth) {
        UserInfo user = new UserInfo() {
            UserName = Email, FullName = FullName, Email = Email, Enabled = true, IsEditor = false, IsGlobalAdministrator = false, UserIsExternal = false, UserIsHidden = false, UserIsDomain = false
        };
        user.SetValue("UserDateOfBirth", UserDateOfBirth); 
        user.PreferredCultureCode = CMSContext.PreferredCultureCode;
        user.UserCreated = DateTime.Now;
        CMS.SiteProvider.UserInfoProvider.SetUserInfo(user);
        UserInfoProvider.AddUserToSite(user.UserName, CMSContext.CurrentSite.SiteName);
        UserInfoProvider.SetPassword(user.UserName, "12345678");
    }
    public static void UserRoleCreated(int UserID, string RoleName) {
        UserInfoProvider.AddUserToRole(UserInfoProvider.GetUserInfo(UserID).UserName, RoleName, CMSContext.CurrentSite.SiteName);
    }
    public static void UserUpdated(int UserID, string Email, string FullName, DateTime UserDateOfBirth) {
        UserInfo user = UserInfoProvider.GetUserInfo(UserID);
        user.Email = Email;
        user.FullName = FullName;
        user.SetValue("UserDateOfBirth", UserDateOfBirth);        
        CMS.SiteProvider.UserInfoProvider.SetUserInfo(user);
    }
    public static void UserDeleted(int UserID){
        UserInfoProvider.DeleteUser(UserID);
    }
    public static void UserRoleDeleted(int UserID, string RoleName) {
        UserInfoProvider.RemoveUserFromRole(UserInfoProvider.GetUserInfo(UserID).UserName, RoleName, CMSContext.CurrentSite.SiteName);
    }
    public static List<RoleInfoObject> RoleList(){
        return RoleInfoProvider.GetAllRoles("RoleID > 13").Tables[0].AsEnumerable().Select(s => new RoleInfoObject {
            RoleID = s.Field<int>("RoleID"), RoleName = s.Field<string>("RoleName"), RoleDisplayName = s.Field<string>("RoleDisplayName"), RoleDescription = s.Field<string>("RoleDescription")
        }).ToList();
    }
    public static void RoleCreated(string RoleDisplayName, string RoleDescription) {
        RoleInfo role = new RoleInfo();
        role.RoleName = ConvertToUnSign(RoleDisplayName);
        role.DisplayName = RoleDisplayName;
        role.Description = RoleDescription;
        role.SiteID = CMSContext.CurrentSite.SiteID;
        if (!CMS.SiteProvider.RoleInfoProvider.RoleExists(role.RoleName, CMSContext.CurrentSite.SiteName))
            CMS.SiteProvider.RoleInfoProvider.SetRoleInfo(role);
    }
    public static void RoleUpdated(string RoleName, string RoleDisplayName, string RoleDescription) {
        RoleInfo role = RoleInfoProvider.GetRoleInfo(RoleName, CMSContext.CurrentSite.SiteName);
        if (role != null) {
            role.RoleName = ConvertToUnSign(RoleDisplayName);
            role.DisplayName = RoleDisplayName;
            role.Description = RoleDescription;
            if (!CMS.SiteProvider.RoleInfoProvider.RoleExists(role.RoleName, CMSContext.CurrentSite.SiteName))
                CMS.SiteProvider.RoleInfoProvider.SetRoleInfo(role);
        }
    }
    public static void RoleDeleted(string RoleName){
        RoleInfoProvider.DeleteRole(RoleName, CMSContext.CurrentSite.SiteName);
    }
    public List<PermissionsObject> PermissionsList(string RoleName, string RoleGroup) {
        return PermissionNameInfoProvider.GetPermissionNames("ResourceID = " + ResourceInfoProvider.GetResourceInfo(RoleGroup).ResourceId, "", 0, "PermissionID,PermissionDisplayName,PermissionName").Tables[0].AsEnumerable().Where(w => !w.Field<string>("PermissionName").Contains("CongDoan"))
            .GroupJoin(RolePermissionInfoProvider.GetRolePermissions("RoleID = " + RoleInfoProvider.GetRoleInfo(RoleName, CMSContext.CurrentSite.SiteName).RoleID, "", 0, "RoleID,PermissionID").Tables[0].AsEnumerable(), pn => pn.Field<int>("PermissionID"), rp => rp.Field<int>("PermissionID"), (pn, rp) => new { pn, rp })
            .SelectMany(sm => sm.rp.DefaultIfEmpty<DataRow>(), (sm, rp) => new PermissionsObject { PermissionID = sm.pn.Field<int>("PermissionID"), PermissionDisplayName = sm.pn.Field<string>("PermissionDisplayName"), PermissionAllow = rp == null ? false : true }).ToList();
    }
    public List<PermissionsObject> PermissionsListNd(string RoleName, string RoleGroup) {
        return PermissionNameInfoProvider.GetPermissionNames("ResourceID = " + ResourceInfoProvider.GetResourceInfo(RoleGroup).ResourceId, "", 0, "PermissionID,PermissionDisplayName,PermissionName").Tables[0].AsEnumerable().Where(w => w.Field<string>("PermissionName").Contains("CongDoan"))
            .GroupJoin(RolePermissionInfoProvider.GetRolePermissions("RoleID = " + RoleInfoProvider.GetRoleInfo(RoleName, CMSContext.CurrentSite.SiteName).RoleID, "", 0, "RoleID,PermissionID").Tables[0].AsEnumerable(), pn => pn.Field<int>("PermissionID"), rp => rp.Field<int>("PermissionID"), (pn, rp) => new { pn, rp })
            .SelectMany(sm => sm.rp.DefaultIfEmpty<DataRow>(), (sm, rp) => new PermissionsObject { PermissionID = sm.pn.Field<int>("PermissionID"), PermissionDisplayName = sm.pn.Field<string>("PermissionDisplayName"), PermissionAllow = rp == null ? false : true }).ToList();
    }
    public List<PermissionsObject> PermissionsListRd(string RoleName, int RoleGroupId) {
        string PermissionName = PermissionNameInfoProvider.GetPermissionNameInfo(RoleGroupId).PermissionName;
        return PermissionNameInfoProvider.GetPermissionNames("ResourceID = " + ResourceInfoProvider.GetResourceInfo(PermissionName).ResourceId, "", 0, "PermissionID,PermissionDisplayName").Tables[0].AsEnumerable()
            .GroupJoin(RolePermissionInfoProvider.GetRolePermissions("RoleID = " + RoleInfoProvider.GetRoleInfo(RoleName, CMSContext.CurrentSite.SiteName).RoleID, "", 0, "RoleID,PermissionID").Tables[0].AsEnumerable(), pn => pn.Field<int>("PermissionID"), rp => rp.Field<int>("PermissionID"), (pn, rp) => new { pn, rp })
            .SelectMany(sm => sm.rp.DefaultIfEmpty<DataRow>(), (sm, rp) => new PermissionsObject { PermissionID = sm.pn.Field<int>("PermissionID"), PermissionDisplayName = sm.pn.Field<string>("PermissionDisplayName"), PermissionAllow = rp == null ? false : true }).ToList();
    }
    public static void PermissionsUpdated(int PermissionID, string RoleName, bool PermissionAllow) {
        if (PermissionAllow)
            RolePermissionInfoProvider.SetRolePermissionInfo(RoleInfoProvider.GetRoleInfo(RoleName, CMSContext.CurrentSiteID).RoleID, PermissionID);
        else
            RolePermissionInfoProvider.DeleteRolePermissionInfo(RoleInfoProvider.GetRoleInfo(RoleName, CMSContext.CurrentSiteID).RoleID, PermissionID);
    }
    public static string ConvertToUnSign(string text) {
        for (int i = 33; i < 48; i++)
            text = text.Replace(((char)i).ToString(), "");
        for (int i = 58; i < 65; i++)
            text = text.Replace(((char)i).ToString(), "");
        for (int i = 91; i < 97; i++)
            text = text.Replace(((char)i).ToString(), "");
        for (int i = 123; i < 127; i++)
            text = text.Replace(((char)i).ToString(), "");
        text = text.Replace(" ", "-");
        var regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
        string strFormD = text.Normalize(NormalizationForm.FormD);
        return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
    }
}