# Samples #

## Associative array params ##

```
        private static void SetupCommandForGetBulkParams(string[] phoneNumbers, string[] paramNames, OracleCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.BindByName = true;

            OracleParameter pMsisdns = new OracleParameter();
            pMsisdns.ParameterName = "P_MSISDN_LIST";
            pMsisdns.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            pMsisdns.Direction = ParameterDirection.Input;
            pMsisdns.Size = phoneNumbers.Length;
            pMsisdns.OracleDbType = OracleDbType.Varchar2;
            pMsisdns.Value = phoneNumbers;

            cmd.Parameters.Add(pMsisdns);

            OracleParameter pParamCodes = new OracleParameter();
            pParamCodes.ParameterName = "p_param_code_list";
            pParamCodes.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            pParamCodes.Direction = ParameterDirection.Input;
            pParamCodes.Size = paramNames.Length;
            pParamCodes.OracleDbType = OracleDbType.Varchar2;
            pParamCodes.Value = paramNames;

            cmd.Parameters.Add(pParamCodes);

            OracleParameter pRefCursorParams = new OracleParameter("p_cur_msisdn_params", OracleDbType.RefCursor);

            pRefCursorParams.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pRefCursorParams);
        }
```

## Output params should have a size ! ##

```

                        OracleParameter p_error_code = new OracleParameter("p_error_code", OracleDbType.Varchar2, 4000);
                        p_error_code.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(p_error_code);

                        OracleParameter p_error_message = new OracleParameter("p_error_message", OracleDbType.Varchar2, 4000);
                        p_error_message.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(p_error_message);

```

## Use CommandBuilder to figure out params peculiarities ##

```
OracleCommandBuilder.DeriveParameters(cmd);
```