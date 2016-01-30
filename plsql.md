# Associative arrays #

## Sample for how to fill ##
```
declare
  -- Non-scalar parameters require additional processing 
  p_msisdn_list sca_opt.t_varchar;
begin
  p_msisdn_list(1) := '420776006951';
  -- Call the procedure
  sca_opt.get_data_roaming_tariff(p_msisdn_list => p_msisdn_list,
                                  p_cur_msisdn_vdp_attrs => :p_cur_msisdn_vdp_attrs,
                                  p_cur_act_raom_tariffs => :p_cur_act_raom_tariffs);
end;
```

where
```
  type t_varchar    is table of VARCHAR2(255) index by binary_integer;
```

# Indexes #

## Function based index ##

```
  CREATE INDEX "FTPRO"."I_FT_FCP_NEW" ON "FTPRO"."FORIS_COMMANDS_PROV" (CASE "STATUS" WHEN 'NEW' THEN "REQ_ID" ELSE NULL END ) 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT)
  TABLESPACE "FTPRO" ;
```

Usage sample:

```
  p_countOfProcessing := 0;
      -- see how many are in the processing status
      select count(1)
        into p_countOfProcessing
        from (select /*+ INDEX_FFS(c I_ft_FCP_PROC) */
               CASE
                 WHEN c.status = 'PROCESSING' THEN
                  c.req_id
                 ELSE
                  NULL
               END req_id,
               c.priority
                from foris_commands_prov c
               where CASE WHEN c.status = 'PROCESSING' THEN c.req_id ELSE NULL END = c.req_id and c.status = 'PROCESSING' and c.command_type in (1, 2, 3, 6, 7, 8, 9, 10, 11, 12, 13, 15, 17)) cmd
       where cmd.priority >= p_Partition - 1000
         and cmd.priority < p_Partition;

      if (p_countOfProcessing < 30) then
        select req_id, priority
          into v_req_id, v_priority
          from (select /*+ INDEX_FFS(c I_ft_FCP_NEW) */
                 CASE
                   WHEN c.status = 'NEW' THEN
                    c.req_id
                   ELSE
                    NULL
                 END req_id,
                 c.priority,
                 c.phone_number,
                 c.command_type
                  from foris_commands_prov c
                 where CASE WHEN c.status = 'NEW' THEN c.req_id ELSE NULL END = c.req_id and status = 'NEW' and c.priority >= p_Partition - 1000 and c.priority < p_Partition and c.command_type in (1, 2, 3, 6, 7, 8, 9, 10, 11, 12, 13, 15, 17)
                 order by c.priority asc, c.req_id asc) cmd
         where 1 = 1
              /*    and cmd.priority >= p_Partition - 1000
                                                                                 and cmd.priority < p_Partition*/
              --and cmd.command_type = p_CommandType
           and (mod(to_number(cmd.phone_number), p_mod_base) =
               p_mod_remainder or cmd.phone_number is null)
           and cmd.phone_number not in
               (select phone_number
                  from (select /*+ INDEX_FFS(c I_ft_FCP_PROC) */
                         CASE
                           WHEN c.status = 'PROCESSING' THEN
                            c.req_id
                           ELSE
                            NULL
                         END req_id,
                         c.priority,
                         c.phone_number
                          from foris_commands_prov c
                         where CASE WHEN c.status = 'PROCESSING' THEN c.req_id ELSE NULL END = c.req_id and status = 'PROCESSING') cmd
                 where 1 = 1
                   and cmd.priority >= p_Partition - 1000
                   and cmd.priority < p_Partition)
           and rownum = 1;
      end if;

```