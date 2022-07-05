# EpiserverCmsAudit

To access the Audit-view, you will need to add a virtual role named "AuditAdmins" as adding virtual roles makes it possible to make the assignment of roles more flexible:

    <episerver.framework>
    <virtualRoles addClaims="true">
      <providers>
        ...
        <add name="AuditAdmins" roles="CmsAdmins" mode="Any" type="EPiServer.Security.MappedRole, EPiServer" />
      </providers>
    </virtualRoles>
  </episerver.framework>

And then create a group in the CMS called AuditAdmins and assign the relevant CMS users to the group. If you're already logged in, try logging back out and in to see the tab.
