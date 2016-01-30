# CM #
```
-- find all charging units for the parent element
select cu.charging_key, ampi.pending_status from cm.element e 
       join cm.charging_unit cu on cu.subscription_id = e.id_subscription
       join cm.abstract_mp_instance ampi on ampi.id = cu.mp_subscription_instanc417
       where 1=1
and e.parent_element_id = 474635 
and e.end_date > sysdate
and cu.end_date > sysdate
and ampi.end_date > sysdate
order by cu.charging_key
```