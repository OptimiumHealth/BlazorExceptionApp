using Material.Blazor;

using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorExceptionApp.Pages
{
    public partial class Dashboard
    {
        #region members
        private class Person
        {
            public Guid UniqueIdentifier { get; set; }
            public string Salutation { get; set; }
            public string GivenName { get; set; }
            public string FamilyName { get; set; }

            public override string ToString()
            {
                return $"{Salutation} {GivenName} {FamilyName}";
            }
        }

        private Person[] People =
    {
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Prof", GivenName = "Marie", FamilyName = "Curie" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Prof", GivenName = "Albert", FamilyName = "Einstein" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Prof", GivenName = "Andrew", FamilyName = "Huxley" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "Bob", FamilyName = "Dylan" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "Barack", FamilyName = "Obama" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Ms", GivenName = "Nadine", FamilyName = "Gordimer" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "Muhammad", FamilyName = "Yunus" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "RtHon", GivenName = "Lord", FamilyName = "Rayleigh" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Ms", GivenName = "Grazia", FamilyName = "Deledda" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "Jean-Paul", FamilyName = "Sartre" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Prof", GivenName = "Esther", FamilyName = "Duflo" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Prof", GivenName = "Yoshinori", FamilyName = "Ohsumi" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Prof", GivenName = "Robert", FamilyName = "Merton" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "Steven", FamilyName = "McClintock" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "Joseph", FamilyName = "McClintock" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Prof", GivenName = "Barbara", FamilyName = "McClintock" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "Boris", FamilyName = "Pasternak" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "Willy", FamilyName = "Brandt" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "Isaac", FamilyName = "Bashevis Singer" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Ms", GivenName = "Olga", FamilyName = "Tokarczuk" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "Günter", FamilyName = "Grass" },
        new Person() { UniqueIdentifier=Guid.NewGuid(), Salutation = "Mr", GivenName = "John", FamilyName = "Hume" },
    };

        private IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, Person>>>> PeopleAsGroupedDataWithGroupList;
        private List<string> GroupOrderedList = new();
        private List<MBGridColumnConfiguration<Person>> ColumnConfigurations { get; set; } = null;

        [Inject] private IMBToastService ToastService { get; set; }

        #endregion

        #region ctor

        public Dashboard()
        {
            GroupOrderedList.Add("Dr");
            GroupOrderedList.Add("Mr");
            GroupOrderedList.Add("Ms");
            GroupOrderedList.Add("Prof");
            GroupOrderedList.Add("Professor");
            GroupOrderedList.Add("RtHon");

            PeopleAsGroupedDataWithGroupList = new MBGrid_DataHelper<Person>().PrepareGridData(
                People,
                typeof(Person).GetProperty("UniqueIdentifier"),
                typeof(Person).GetProperty("FamilyName"),
                orderPropertyInfo2: typeof(Person).GetProperty("GivenName"),
                group: true,
                groupPropertyInfo: typeof(Person).GetProperty("Salutation"),
                groupItemEnumerable: GroupOrderedList
                );

            ColumnConfigurations = new List<MBGridColumnConfiguration<Person>>();

            ColumnConfigurations.Add(new MBGridColumnConfiguration<Person>(
                dataExpression: c => c.GivenName,
                title: "Given name",
                width: 10));
            ColumnConfigurations.Add(new MBGridColumnConfiguration<Person>(
                dataExpression: c => c.FamilyName,
                title: "Family name",
                width: 15));
            ColumnConfigurations.Add(new MBGridColumnConfiguration<Person>(
                width: 75));
        }
        #endregion

        #region OnInitializedAsync
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync();
//                throw new Exception("Meaningless exception that should close the circuit in OIA");
            }
            catch (Exception ex)
            {
                ToastService.ShowToast(
                    heading: "EXCEPTION",
                    message: ex.ToString(),
                    level: MBToastLevel.Error,
                    showIcon: true);
                await Task.Delay(2000);
                throw;
            }
        }
        #endregion

        #region GridRowClicked
        protected async Task MBGridRowClicked(string caseIdentifier)
        {
            try
            {
                await Task.CompletedTask;
                throw new Exception("Meaningless exception that should close the circuit in MBGRC");
            }
            catch (Exception ex)
            {
                ToastService.ShowToast(
                    heading: "EXCEPTION",
                    message: ex.ToString(),
                    level: MBToastLevel.Error,
                    showIcon: true);
                throw;
            }
        }
    }

    #endregion

}
