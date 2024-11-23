import { useState, useEffect } from "react";

export default function Dashboard() {
  const [groups, setGroups] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    // Fetch groups data from backend API.

    // For now, just simulate dummy data
    setTimeout(() => {
      setGroups([
        { id: 1, name: "Family" },
        { id: 2, name: "Friends" },
        { id: 3, name: "Football" },
        { id: 4, name: "Family" },
        { id: 5, name: "Friends" },
        { id: 6, name: "Football" },
      ]);
      setIsLoading(false);
    }, 1000);
  }, []);

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-screen">
        <div className="text-lg font-semibold text-gray-600 animate-pulse">
          Loading groups...
        </div>
      </div>
    );
  }
  if (groups.length === 0) {
    return (
      <div className="flex items-center justify-center h-screen">
        <div className="text-xl font-medium text-red-500">No groups found.</div>
      </div>
    );
  }

  return (
    <div className="min-h-screen px-4 py-8 bg-gradient-to-br from-red-50 to-red-100">
      <div className="max-w-5xl mx-auto ">
        <h1 className="text-3xl font-bold text-gray-800 mb-10 text-center">
          Your Groups
        </h1>
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
          {groups.map((group) => (
            <div
              key={group.id}
              className="rounded-2xl shadow-lg bg-white hover:shadow-xl flex flex-col justify-between transition-shadow p-6"
            >
              <h2 className="text-xl text-gray-700 font-semibold mb-2">
                {group.name}
              </h2>
              <p className="text-sm text-gray-500 mb-4">Group ID: {group.id}</p>
              <a
                href={`groups/${group.id}`}
                className="bg-indigo-600 text-center hover:bg-indigo-700 transition duration-200 rounded-lg px-4 py-2 text-white font-semibold"
              >
                View Group
              </a>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
