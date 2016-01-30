# Introduction #

Reading notebook.
Specs: http://www.ibm.com/developerworks/library/specification/ws-fed/

20 minutes path: http://msdn.microsoft.com/en-us/library/bb498017.aspx

Small sample for implementing Secure Token Service with wcf: http://weblogs.asp.net/cibrax/archive/2006/03/14/440222.aspx

## Microsoft "Geneva" framework ##
http://msdn.microsoft.com/en-us/security/aa570351.aspx

White paper on Beta 1: http://download.microsoft.com/download/7/d/0/7d0b5166-6a8a-418a-addd-95ee9b046994/GenevaFrameworkBeta1_Whitepaper_KBrown_FINAL.docx

Download and links on connect: https://connect.microsoft.com/site/sitehome.aspx?SiteID=642




# Components and collaborators. #

Requestor

Resource

## Attribute service ##
When requestors interact with resources in different trust realms (or different parts of a
federation), there is often a need to know additional information about the requestor in
order to authorize, process, or personalize the experience. A service, known as an Attribute Service may be available within a realm or federation. As such, an attribute service is used to provide the attributes about a requestor that are relevant to the completion of a request, given that the service is authorized to obtain this information. This approach allows the sharing of data between authorized entities.

_Sample collaboration scenario:_

Resource in realm B obtains Requestor's additional attributes (e.g. Requestor customization requirements or personal information) in the realm A of Requestor.

## Pseudonym service ##
To facilitate single sign-on where multiple identities need to be automatically mapped and
the privacy of the principal needs to be maintained, there may also be a pseudonym
service. A pseudonym service allows a principal to have different aliases at different
resources/services or in different realms, and to optionally have the pseudonym change perservice or per-login. While some scenarios support identities that are trusted as presented, pseudonyms services allow those cases where identity mapping needs to occur between an identity and a pseudonym on behalf of the principal.

_Sample collaboration scenario:_

Requestor in realm A obtains an alias from the Pseudonym service in its own realm for accessing the resource in the realm B. This alias is then provided to STS in realm B.

# Some troubleshooting #
Setting up the stateful security context token: http://msdn.microsoft.com/en-us/library/ms731814.aspx (also notes the need for the local profile)