1. Database entities go in .Data/Models
2. View DTOs go into the handler that uses them.
	- if they are used on multiple handlers - add them to Features/{FeatureName}/Models.
	- if they are used in many places across the whole app add them to Features/Common/Models