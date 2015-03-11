namespace NavigationDrawer
{
	public class Post
	{
		public long id{ get; set; }
		public string content{ get; set; }
		public string name{ get; set; }
		public string project{ get; set; }
		public string client{ get; set; }
		public string date{ get; set; }
		public string photo{ get; set; }


		public Post (long id, string photo, string content, string name, string project, string client, string date)
		{
			this.id = id;
			this.content = content;
			this.photo=photo;
			this.name = name;
			this.project = project;
			this.client = client;
			this.date = date;
		}

		public Post(Post prev)
		{
			id = prev.id;
			photo = prev.photo;
			name = prev.name;
			content = prev.content;
			project = prev.project;
			date = prev.date;
		}
	}
}


