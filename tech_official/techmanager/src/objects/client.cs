namespace NavigationDrawer
{
	public class Client
	{
		public long id { get; set; }
		public string photo{ get; set; }
		public string name{ get; set; }
		public string contactName{ get; set; }
		public string contactEmail{get;set;}

		public Client(long id, string photo,string name,string contactName,string contactEmail)
		{
			this.id = id;
			this.photo = photo;
			this.name = name;
			this.contactName = contactName;
			this.contactEmail = contactEmail;
		}

		public Client(Client prev)
		{
			id = prev.id;
			photo = prev.photo;
			name = prev.name;
			contactName = prev.contactName;
			contactEmail = prev.contactEmail;
		}
	}
}

