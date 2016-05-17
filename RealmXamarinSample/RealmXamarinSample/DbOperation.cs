using Realms;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RealmXamarinSample
{
	public class Subscription : RealmObject
	{
		[ObjectId]
		public string Id { get; set; }

		public DateTimeOffset Created { get; set; }

		public string Tag { get; set; }

		public string Description { get; set; }

		public string Url { get; set; }
	}

	public class Group : RealmObject
	{
		[ObjectId]
		public string Id { get; set; }

		public string Name { get; set; }

		public RealmList<Subscription> Subscriptions { get; }
	}

	public class DbOperation
	{
		public void Clear()
		{
			var config = new RealmConfiguration();
			Debug.WriteLine($"Path = {config.DatabasePath}");
			Realm.DeleteRealm(config);
		}

		public void Create()
		{
			using (var realm = Realm.GetInstance())
			{
				realm.Write(() =>
				{
					var s1 = realm.CreateObject<Subscription>();
					s1.Id = "s1";
					s1.Created = new DateTimeOffset(2016, 5, 15, 14, 45, 0, 0, TimeSpan.Zero);
					s1.Tag = "Xamarin";
					s1.Description = "Xamarin.Forms";
					s1.Url = "https://github.com/xamarin/Xamarin.Forms";

					var s2 = realm.CreateObject<Subscription>();
					s2.Id = "s2";
					s2.Created = new DateTimeOffset(2016, 5, 15, 14, 48, 0, 0, TimeSpan.Zero);
					s2.Tag = "Xamarin";
					s2.Description = "xamarin-android";
					s2.Url = "https://github.com/xamarin/xamarin-android";

					var s3 = realm.CreateObject<Subscription>();
					s3.Id = "s3";
					s3.Created = new DateTimeOffset(2016, 5, 15, 14, 51, 0, 0, TimeSpan.Zero);
					s3.Tag = "Xamarin";
					s3.Description = "Xamarin.Auth";
					s3.Url = "https://github.com/xamarin/Xamarin.Auth";

					var g1 = realm.CreateObject<Group>();
					g1.Id = "g1";
					g1.Name = "Xamarin URL";
					g1.Subscriptions.Add(s1);
					g1.Subscriptions.Add(s2);
					g1.Subscriptions.Add(s3);

					var s4 = realm.CreateObject<Subscription>();
					s4.Id = "s4";
					s4.Created = new DateTimeOffset(2016, 5, 15, 14, 55, 0, 0, TimeSpan.Zero);
					s4.Tag = "Realm";
					s4.Description = "realm-java";
					s4.Url = "https://github.com/realm/realm-java";

					var s5 = realm.CreateObject<Subscription>();
					s5.Id = "s5";
					s5.Created = new DateTimeOffset(2016, 5, 15, 14, 56, 0, 0, TimeSpan.Zero);
					s5.Tag = "Realm";
					s5.Description = "realm-cocoa";
					s5.Url = "https://github.com/realm/realm-cocoa";

					var g2 = realm.CreateObject<Group>();
					g2.Id = "g2";
					g2.Name = "Realm URL";
					g2.Subscriptions.Add(s4);
					g2.Subscriptions.Add(s5);

					var s6 = realm.CreateObject<Subscription>();
					s6.Id = "s6";
					s6.Created = new DateTimeOffset(2016, 5, 14, 20, 01, 0, 0, TimeSpan.Zero);
					s6.Tag = "";
					s6.Description = "GitHub";
					s6.Url = "https://github.com/";
				});
			}
		}

		public void SelectAll(out string message)
		{
			using (var realm = Realm.GetInstance())
			{
				var all = realm.All<Subscription>();

				var sb = new StringBuilder();
				sb.AppendLine("*** All ***");

				foreach (var s in all)
				{
					sb.AppendLine($"Id = {s.Id}, Created = {s.Created}, Tag = {s.Tag}, Description = {s.Description}, Url = {s.Url}");
				}

				Debug.WriteLine(sb.ToString());

				message = sb.ToString();
			}
		}

		public void SelectXamarin(out string message)
		{
			using (var realm = Realm.GetInstance())
			{
				var xamarin = realm.All<Subscription>().Where(s => s.Tag == "Xamarin");

				var sb = new StringBuilder();
				sb.AppendLine("*** Tag = Xamarin ***");

				foreach (var s in xamarin)
				{
					sb.AppendLine($"Id = {s.Id}, Created = {s.Created}, Tag = {s.Tag}, Description = {s.Description}, Url = {s.Url}");
				}

				Debug.WriteLine(sb.ToString());

				message = sb.ToString();
			}
		}

		public void SelectSort(out string message)
		{
			using (var realm = Realm.GetInstance())
			{
				var sort = realm.All<Subscription>().OrderByDescending(s => s.Created);

				var sb = new StringBuilder();
				sb.AppendLine("*** Sort Creared ***");

				foreach (var s in sort)
				{
					sb.AppendLine($"Id = {s.Id}, Created = {s.Created}, Tag = {s.Tag}, Description = {s.Description}, Url = {s.Url}");
				}

				Debug.WriteLine(sb.ToString());

				message = sb.ToString();
			}
		}

		public void SelectNotEmpty(out string message)
		{
			using (var realm = Realm.GetInstance())
			{
				var notEmpty = realm.All<Subscription>().Where(s => s.Tag != "");

				var sb = new StringBuilder();
				sb.AppendLine("*** Tag != \"\" ***");

				foreach (var s in notEmpty)
				{
					sb.AppendLine($"Id = {s.Id}, Created = {s.Created}, Tag = {s.Tag}, Description = {s.Description}, Url = {s.Url}");
				}

				Debug.WriteLine(sb.ToString());

				message = sb.ToString();
			}
		}

		public void SelectGroup(out string message)
		{
			using (var realm = Realm.GetInstance())
			{
				var group = realm.All<Group>();

				var sb = new StringBuilder();
				sb.AppendLine("*** Group ***");

				foreach (var g in group)
				{
					sb.AppendLine($"Group [{g.Name}]");
					foreach (var s in g.Subscriptions)
					{
						sb.AppendLine($"Id = {s.Id}, Created = {s.Created}, Tag = {s.Tag}, Description = {s.Description}, Url = {s.Url}");
					}
				}

				Debug.WriteLine(sb.ToString());

				message = sb.ToString();
			}
		}

		public void Update()
		{
			using (var realm = Realm.GetInstance())
			{
				var s6 = realm.All<Subscription>().Where(s => s.Id == "s6").First();

				realm.Write(() =>
				{
					s6.Created = new DateTimeOffset(2016, 5, 15, 16, 37, 0, 0, TimeSpan.Zero);
				});
			}
		}

		public void Delete()
		{
			using (var realm = Realm.GetInstance())
			{
				var s2 = realm.All<Subscription>().Where(s => s.Id == "s2").First();

				realm.Write(() =>
				{
					realm.Remove(s2);
				});
			}
		}
	}
}

