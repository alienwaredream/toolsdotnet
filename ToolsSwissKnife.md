# Given user account, show SID #
## Justification ##
For range of tasks knowing SID of a user is required. For example SDDL takes SID as user or group identifier.

## Usage ##
tools.swissknife gus "account"

## Sample ##


Code behind:
> NTAccount account = new NTAccount(args[0](0.md));             System.Console.WriteLine("SID: " + account.Translate(typeof(System.Security.Principal.SecurityIdentifier)).Value);