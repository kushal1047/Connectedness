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
    <div className="min-h-screen px-4 py-8 bg-gray-100">
      <div className="max-w-3xl mx-auto ">
        <h1 className="text-3xl font-bold text-gray-800 mb-6 text-center">
          Your Groups
        </h1>
        <ul className="space-y-4">
          {groups.map((group) => (
            <li
              key={group.id}
              className="bg-white p-5 shadow hover:shadow-md transition-shadow border-l-4 border-blue-500 rounded-2xl"
            >
              <span className="text-lg font-medium text-gray-700">
                {group.name}
              </span>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}
