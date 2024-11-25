import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";

export default function GroupDetails() {
  const { groupId } = useParams();
  const [groupName, setGroupName] = useState("Loading...");
  const [members, setMembers] = useState([]);

  useEffect(() => {
    setGroupName("Maarey FC");
    setMembers([
      {
        userId: 1,
        fullName: "Kushal Niraula",
        totalAnswered: 8,
        totalCreated: 2,
        fullParticipated: false,
      },
      {
        userId: 2,
        fullName: "Sam DC",
        totalAnswered: 7,
        totalCreated: 1,
        fullyParticipated: false,
      },
      {
        userId: 2,
        fullName: "Pratik Dhakal",
        totalAnswered: 9,
        totalCreated: 4,
        fullyParticipated: true,
      },
    ]);
  }, [groupId]);
  return (
    <div className="min-h-screen px-4 py-8">
      <div className="max-w-4xl mx-auto">
        <h1 className="text-3xl font-bold text-gray-800 text-center mb-10">
          Group: {groupName}
        </h1>
        <div className="bg-gray-100 rounded-xl shadow-md p-4">
          <h2 className="text-xl font-semibold mb-4 text-gray-700">Members</h2>
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-300">
              <thead className="bg-gray-200">
                <tr>
                  <th className="px-6 py-3 text-left uppercase text-xs font-medium text-gray-700 tracking-wider">
                    Name
                  </th>
                  <th className="text-xs text-left px-6 py-3 uppercase text-gray-700 tracking-wider font-medium">
                    Questions Created
                  </th>
                  <th className="text-xs text-gray-700 font-medium px-6 py-3 uppercase tracking-wider text-left">
                    Questions Answered
                  </th>
                  <th className="text-xs text-gray-700 font-medium text-left uppercase tracking-wider px-6 py-3">
                    Participation
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {members.map((member) => (
                  <tr>
                    <td className="px-6 py-4 text-sm text-gray-900 whitespace-nowrap">
                      {member.fullName}
                    </td>
                    <td className="text-sm px-6 py-4 whitespace-nowrap text-blue-600">
                      {member.totalCreated}
                    </td>
                    <td className="text-sm text-green-600 px-6 py-4 whitespace-nowrap">
                      {member.totalAnswered}
                    </td>
                    <td className="text-sm whitespace-nowrap px-6 py-4">
                      {member.fullyParticipated ? (
                        <span className="text-green-700">
                          ✅ Fully Participated
                        </span>
                      ) : (
                        <span>❌ Incomplete</span>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}
