**One or more associated objects were passed for collection property  on type , but the target collection is null.**

```
        private IList<Goal> goals = new List<Goal>();

        [Composition]
        [Include, Association("ActionPlan_Goal", "Id", "ActionPlanId")]
        public IList<Goal> Goals { get { return goals; } set { goals = value; } }
```

Good example on composition here: http://systemmetaphor.blogspot.com/2010/02/wcf-ria-services-advanced-topics-fully.html or here http://blogs.msdn.com/digital_ruminations/archive/2009/11/18/composition-support-in-ria-services.aspx


**System.Windows.Ria.DomainOperationException: Load operation failed for query 'GetActionPlan'. The server did not provide a meaningful reply; this might be caused by a contract mismatch, a premature session shutdown or an internal server error.**