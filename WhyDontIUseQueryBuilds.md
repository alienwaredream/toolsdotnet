
```
public IBuildQueryResult[] QueryBuilds(IBuildDetailSpec[] buildDetailSpecs)
{
    BuildQueryResult[] resultArray = null;
    if (this.BuildServerVersion == BuildServerVersion.V1)
    {
        List<BuildQueryResult> list = new List<BuildQueryResult>();
        foreach (IBuildDetailSpec spec in buildDetailSpecs)
        {
            string teamProject = spec.DefinitionSpec.TeamProject;
            string name = spec.DefinitionSpec.Name;
            if (TFStringComparer.BuildName.Equals(name, cWildcard))
            {
                name = string.Empty;
            }
            BuildData[] listOfBuilds = this.BuildStore.GetListOfBuilds(teamProject, name);
            List<BuildDetail> list2 = new List<BuildDetail>();
            for (int j = 0; j < listOfBuilds.Length; j++)
            {
                if (TFStringComparer.BuildNumber.Equals(spec.BuildNumber, cWildcard) || TFStringComparer.BuildNumber.Equals(spec.BuildNumber, listOfBuilds[j].BuildNumber))
                {
                    listOfBuilds[j].TeamProject = teamProject;
                    list2.Add(new BuildDetail(this, listOfBuilds[j]));
                }
            }
            list.Add(new BuildQueryResult(list2.ToArray()));
        }
        resultArray = list.ToArray();
    }
    else
    {
        BuildDetailSpec[] destinationArray = new BuildDetailSpec[buildDetailSpecs.Length];
        Array.Copy(buildDetailSpecs, destinationArray, buildDetailSpecs.Length);
        resultArray = this.BuildService.QueryBuilds(destinationArray);
    }
    for (int i = 0; i < buildDetailSpecs.Length; i++)
    {
        Comparison<BuildDetail> comparison = null;
        switch (buildDetailSpecs[i].QueryOrder)
        {
            case BuildQueryOrder.StartTimeAscending:
                comparison = delegate (BuildDetail left, BuildDetail right) {
                    return left.StartTime.CompareTo(right.StartTime);
                };
                break;

            case BuildQueryOrder.StartTimeDescending:
                comparison = delegate (BuildDetail left, BuildDetail right) {
                    return -1 * left.StartTime.CompareTo(right.StartTime);
                };
                break;

            case BuildQueryOrder.FinishTimeAscending:
                comparison = delegate (BuildDetail left, BuildDetail right) {
                    return left.FinishTime.CompareTo(right.FinishTime);
                };
                break;

            case BuildQueryOrder.FinishTimeDescending:
                comparison = delegate (BuildDetail left, BuildDetail right) {
                    return -1 * left.FinishTime.CompareTo(right.FinishTime);
                };
                break;

            default:
                comparison = delegate (BuildDetail left, BuildDetail right) {
                    return left.StartTime.CompareTo(right.StartTime);
                };
                break;
        }
        Array.Sort<BuildDetail>(resultArray[i].m_builds, comparison);
    }
    return resultArray;
}


```