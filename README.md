**README**

To execute the solution just make sure docker-compose project is set as the StartUp project and click in the green arrow, as seen below, to run the application.

![image](https://github.com/edissonmachado/MedicCare/assets/942767/defd8bdf-6dae-49b9-9ab1-223b4cabc7d0)

Once the execution starts, two docker containers will start running. One container will host the application and the other will host the Postgres database. This database will be pre-populated with the script you can find in **docker-compose\init.sql**.

For the application you can find two endpoints for the Patient entity, both in the PatientController from the **MedicCare.Presentation\MedicCare.Api** project. These endpoits are:
1. **GetById:** which is intended to be there just to show how the generic repository is used. The rout for the endpoint is **/api/patients/{id}**
2. **GetEncounters:** which cover the conditions stated below:

Return a list of patients with three fields:
- first name and last name in the &quot;last name, first name&quot; format, example: Sinatra, Frank
- comma-separated list of cities where this patient has ever visited a facility, example:
Philadelphia, New York, Boston
- category: A if age is less than 16, B otherwise

  Only include patients who has at least two encounters insured by companies from different cities.

  Show patients with the smallest number of encounters first.


**Developer notes:**

  This project was based on Clean Architecture principles and CQRS was implemented. It can be seen as a bit overengineered, but it was done this way to show some of the learned skills in my dev career.

  There is a know issue regarding the database naming, but the application works flawlessly anyways.
